namespace MyCarsDb.Server.WebApi.Controllers.Base
{
    using System.Net.Http;
    using System.Web.Http;    

    using Microsoft.AspNet.Identity.Owin;

    using MyCarsDb.Data;

    public abstract class BaseController : ApiController
    {
        protected MyCarsDbContext _dbContext;

        public BaseController()
        {
            _dbContext = Request.GetOwinContext().Get<MyCarsDbContext>();           
        }

        public BaseController(MyCarsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
