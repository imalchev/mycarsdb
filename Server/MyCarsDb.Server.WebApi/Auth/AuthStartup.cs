namespace MyCarsDb.Server.WebApi.Auth
{
    using System;

    using Microsoft.Owin;
    using Microsoft.Owin.Security.OAuth;

    using Owin;

    public class AuthStartup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        public static void ConfigureAuth(IAppBuilder app)
        {
            // Configure the web api token endpoint

            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                // Endpoint address
                TokenEndpointPath = new PathString("/Users/Login"),

                // Configure the application for OAuth based flow
                Provider = new MyCarsDbOAuthAuthorizationServerProvider(PublicClientId),

                // Default access tokens are valid for about 1 month
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(30),

                // TODO: Set to false in production !
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