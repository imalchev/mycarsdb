namespace MyCarsDb.Web.Infrastructure.Config
{
    using System;

    using Microsoft.Owin;
    using Microsoft.Owin.Cors;
    using Microsoft.Owin.Security.OAuth;

    using Owin;

    using MyCarsDb.Web.Infrastructure.Auth;
    using MyCarsDb.Business.Managers.Contracts;

    public class AuthConfig
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        public static void ConfigureAuth(IAppBuilder app, Func<IIdentityManager> identityManagerFactory)
        {
            // Configure the web api token endpoint
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                // Endpoint address
                TokenEndpointPath = new PathString("/api/Users/Login"),

                // Configure the application for OAuth based flow
                Provider = new MyCarsDbOAuthAuthorizationServerProvider(PublicClientId, identityManagerFactory),

                // Default access tokens are valid for about 1 month
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(30),

                // TO DO: Set to false in production !
                AllowInsecureHttp = true,
            };
            app.UseOAuthAuthorizationServer(OAuthOptions);

            // Configure the sockets tokens endpoint
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
            {
                Provider = new MyCarsDbOAuthBearerAuthenticationProvider()
            });
        }
    }
}