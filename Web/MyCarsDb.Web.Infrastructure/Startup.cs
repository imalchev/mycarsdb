[assembly: Microsoft.Owin.OwinStartup(typeof(MyCarsDb.Web.Infrastructure.Startup))]
namespace MyCarsDb.Web.Infrastructure
{
    using System;
    using System.Reflection;
    using System.Web.Http;

    using Microsoft.Owin;
    using Microsoft.Owin.Cors;

    using Owin;

    using SimpleInjector;

    using MyCarsDb.Business.Managers.Contracts;
    using MyCarsDb.Web.Infrastructure.Config;    

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            DatabaseConfig.Initialize();

            // TODO: this assemblies should go to constants 
            var mappingsAssembly = Assembly.Load("MyCarsDb.Common.Mappings");
            AutoMapperConfig.Execute(mappingsAssembly);

            var httpConfig = new HttpConfiguration();

            Container container = SimpleInjectorConfig.Register(app, httpConfig);

            // Рegisters WebApi routes
            WebApiConfig.Register(httpConfig);

            httpConfig.EnsureInitialized();

            // Тhis enables cors for autentication requests
            app.UseCors(CorsOptions.AllowAll);

            // Тhis is factory method that only purpose is to instante identity manager for static login method .....
            Func<IIdentityManager> identityManagerFactory = () => container.GetInstance<IIdentityManager>();
            
            AuthConfig.ConfigureAuth(app, identityManagerFactory);

            app.UseWebApi(httpConfig);
        }
    }
}