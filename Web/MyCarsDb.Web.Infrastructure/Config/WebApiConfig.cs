namespace MyCarsDb.Web.Infrastructure.Config
{    
    using System.Web.Http;
    using System.Web.Http.Cors;

    using MyCarsDb.Web.Api.Infrastructure.Formatters;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services            
            config.Formatters.Clear();
            config.Formatters.Add(new BrowserJsonFormatter());

            // enable cors
            var cors = new EnableCorsAttribute("http://localhost:4200", "*", "*");
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
