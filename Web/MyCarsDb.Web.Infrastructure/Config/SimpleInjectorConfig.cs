namespace MyCarsDb.Web.Infrastructure.Config
{
    using System.Web.Http;

    using Owin;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security.DataProtection;

    using SimpleInjector;
    using SimpleInjector.Extensions.ExecutionContextScoping;
    using SimpleInjector.Integration.WebApi;

    using MyCarsDb.Business.Managers;
    using MyCarsDb.Business.Managers.Identity;
    using MyCarsDb.Business.Managers.Contracts;

    using MyCarsDb.Data.Contracts;
    using MyCarsDb.Data.Models;
    using MyCarsDb.Data.EntityFramework;
    using MyCarsDb.Data.EntityFramework.Contracts;    

    public class SimpleInjectorConfig
    {
        public static Container Register(IAppBuilder app, HttpConfiguration config)
        {
            Container container = new Container();

            // Set the scoped lifestyle one directly after creating the container
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            // identity
            container.Register<IUserManager, UserManager>(Lifestyle.Scoped);
            container.Register<IUserStore<User, int>, UserStore>(Lifestyle.Scoped);
            container.Register<IUserTokenProvider<User, int>>(GetDataProtectionProvider, Lifestyle.Scoped);

            // data
            container.Register<IMyCarsDbData, MyCarsDbData>(Lifestyle.Scoped);
            container.Register<IMyCarsDbContext, MyCarsDbContext>(Lifestyle.Scoped);

            // managers
            container.Register<IIdentityManager, IdentityManager>();
            container.Register<IVehiclesManager, VehiclesManager>();

            // web api controllers
            container.RegisterWebApiControllers(config);

            container.Verify();

            // add middleware
            app.Use(async (context, next) => 
            {
                using (container.BeginExecutionContextScope())
                {
                    await next();
                }
            });

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            return container;
        }

        private static DataProtectorTokenProvider<User, int> GetDataProtectionProvider()
        {
            // IdentityFactoryOptions<UserManager> options;
            var dataProtectionProvider = new DpapiDataProtectionProvider("IR");

            return new DataProtectorTokenProvider<User, int>(dataProtectionProvider.Create("IR Dpapi protection provider"));
        }
    }
}