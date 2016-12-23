[assembly: Microsoft.Owin.OwinStartup(typeof(MyCarsDb.Server.WebApi.Startup))]

namespace MyCarsDb.Server.WebApi
{
    using System.Web.Http;

    using Microsoft.Owin;

    using Owin;

    using MyCarsDb.Server.WebApi.Config;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            DatabaseConfig.Initialize();

            var httpConfig = new HttpConfiguration();

            //registers WebApi routes
            WebApiConfig.Register(httpConfig);

            httpConfig.EnsureInitialized();

            AuthConfig.ConfigureAuth(app);

            app.UseWebApi(httpConfig);
        }
    }
}