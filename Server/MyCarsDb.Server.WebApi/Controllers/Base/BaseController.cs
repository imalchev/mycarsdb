namespace MyCarsDb.Server.WebApi.Controllers.Base
{
    using System.Net.Http;
    using System.Web.Http;

    using Microsoft.AspNet.Identity.Owin;

    using MyCarsDb.Data;
    using Auth;

    public abstract class BaseController : ApiController
    {
        private MyCarsDbContext _dbContext;
        private MyCarsDbUserManager _userManager;

        public BaseController()
        {
        }

        public BaseController(MyCarsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected MyCarsDbUserManager UserManager
        {
            get { return _userManager ?? Request.GetOwinContext().GetUserManager<MyCarsDbUserManager>(); }
            set { _userManager = value; }
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
