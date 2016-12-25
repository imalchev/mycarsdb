namespace MyCarsDb.Server.WebApi.Controllers
{
    using System.Web.Http;

    using MyCarsDb.Server.WebApi.Controllers.Base;
    using MyCarsDb.Server.WebApi.DataTransferModels;

    [AllowAnonymous]
    public class VehiclesController : BaseController
    {
        [HttpPost]
        public IHttpActionResult Add(VehicleModel model)
        {
            return Ok();
        }
    }
}
