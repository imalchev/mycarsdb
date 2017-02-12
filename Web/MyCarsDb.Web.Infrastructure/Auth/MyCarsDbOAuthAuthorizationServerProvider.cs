namespace MyCarsDb.Web.Infrastructure.Auth
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using Microsoft.Owin.Security.OAuth;
    using Microsoft.Owin.Security;

    using MyCarsDb.Business.Models;
    using MyCarsDb.Business.Managers.Contracts;    

    public class MyCarsDbOAuthAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private const string INVALID_GRANT = "invalid_grant";
        private const string INVALID_CLIENT_ID = "invalid_clientId";

        private readonly string _publicClientId;
        private readonly Func<IIdentityManager> _identityManagerFactory;

        public MyCarsDbOAuthAuthorizationServerProvider(string publicClientId, Func<IIdentityManager> identityManagerFactory)
        {

            if (publicClientId == null)
            {
                throw new ArgumentNullException(nameof(publicClientId));
            }            

            if (identityManagerFactory == null)
            {
                throw new ArgumentNullException(nameof(identityManagerFactory));
            }

            _publicClientId = publicClientId;
            _identityManagerFactory = identityManagerFactory;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
                IIdentityManager identityManager = _identityManagerFactory.Invoke();
                if (identityManager == null)
                {
                    throw new NullReferenceException($"{nameof(identityManager)} can not be found!");
                }

                User user = await identityManager.FindAsync(context.UserName, context.Password);
                if (user == null)
                {
                    context.SetError(INVALID_GRANT, "Invalid username and password");
                    return;
                }

                ICollection<string> inRoles = await identityManager.GetRolesAsync(user.Id);

                // oAuth
                var oAuthIdentity = ClaimsIdentityFactory.Create(user, OAuthDefaults.AuthenticationType, inRoles.ToArray());

                AuthenticationProperties properties = CreateProperties();
                properties.Dictionary.Add("as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId);
                properties.Dictionary.Add("username", context.UserName);

                var ticket = new AuthenticationTicket(oAuthIdentity, properties);
                context.Validated(ticket);
            }
            catch (Exception ex)
            {
                // TODO: some error logging

                throw;
            }
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties()
        {
            return new AuthenticationProperties();
        }
    }
}