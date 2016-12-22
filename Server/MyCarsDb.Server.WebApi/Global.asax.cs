namespace MyCarsDb.Server.WebApi
{
    using System.Web;
    using System.Web.Http;

    using MyCarsDb.Server.WebApi.Config;

    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
