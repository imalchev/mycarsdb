namespace MyCarsDb.Server.WebApi.Config
{
    using System;

    using Microsoft.Owin;
    using Microsoft.Owin.Security.OAuth;

    using Owin;

    using MyCarsDb.Data;
    using MyCarsDb.Server.WebApi.Auth;

    public class AuthConfig
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        public static void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(MyCarsDbContext.Create);
            app.CreatePerOwinContext<MyCarsDbUserManager>(MyCarsDbUserManager.Create);

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