namespace MyCarsDb.Server.WebApi.Auth
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security.OAuth;
    using Microsoft.Owin.Security;

    using MyCarsDb.Data.Models;    

    public class MyCarsDbOAuthAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private const string INVALID_GRANT = "invalid_grant";
        private const string INVALID_CLIENT_ID = "invalid_clientId";

        private readonly string _publicClientId;

        public MyCarsDbOAuthAuthorizationServerProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException(nameof(publicClientId));
            }

            _publicClientId = publicClientId;
        }


        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
                // good place to throw an exception for testing
                // #if TEST ..............

                MyCarsDbUserManager userManager = context.OwinContext.GetUserManager<MyCarsDbUserManager>();
                if (userManager == null)
                {
                    throw new Exception("UserManager can not be found!");
                }

                User user = await userManager.FindAsync(context.UserName, context.Password);
                if (user == null)
                {
                    context.SetError(INVALID_GRANT, "Invalid username and password");
                    return;
                }
                
                IList<string> inRoles = await userManager.GetRolesAsync(user.Id);

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