namespace MyCarsDb.Server.WebApi.Controllers.Base
{
    using System.Net.Http;
    using System.Web.Http;    

    using Microsoft.AspNet.Identity.Owin;

    using MyCarsDb.Data;

    public abstract class BaseController : ApiController
    {
        private MyCarsDbContext _dbContext;

        public BaseController()
        {
        }

        public BaseController(MyCarsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected MyCarsDbContext DbContext
        {
            get
            {
                return _dbContext ?? Request.GetOwinContext().Get<MyCarsDbContext>();
            }
            private set
            {
                _dbContext = value;
            }
        }
    }
}
