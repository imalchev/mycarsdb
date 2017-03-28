namespace MyCarsDb.Web.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;

    using MyCarsDb.Web.Api.Controllers.Base;
    using MyCarsDb.Business.Managers.Contracts;
    using MyCarsDb.Business.Models;

    public class VehicleMakesController : BaseController
    {
        private IVehicleMakesManager _vehicleMakesManager;

        public VehicleMakesController(IVehicleMakesManager vehicleMakesManager)
        {
            if (vehicleMakesManager == null)
            {
                throw new ArgumentNullException(nameof(vehicleMakesManager));
            }

            _vehicleMakesManager = vehicleMakesManager;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetMakes()
        {
            IEnumerable<VehicleMake> makes = await _vehicleMakesManager.GetAllMakesAsync();

            return Ok(makes);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetModelsByMakeId(int id)
        {
            IEnumerable<VehicleModel> models = await _vehicleMakesManager.GetAllModelsByMakeIdAsync(id);

            return Ok(models);
        }
    }
}
