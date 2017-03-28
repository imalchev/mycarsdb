namespace MyCarsDb.Web.Api.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;

    using MyCarsDb.Business.Models;
    using MyCarsDb.Business.Managers.Contracts;
    using MyCarsDb.Web.Api.Controllers.Base;    

    [AllowAnonymous]
    public class VehiclesController : BaseController
    {
        private readonly IVehiclesManager _vehicleManager;

        public VehiclesController(IVehiclesManager vehicleManager)
        {
            _vehicleManager = vehicleManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> Add(Vehicle vehicle)
        {
            // TO DO: validation should go through ModelValidationAttribute

            var username = this.User.Identity.Name;

            await _vehicleManager.AddNewVehicleAsync(vehicle, username);

            // TODO: return some id of 
            return Ok();
        }

        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> Edit(int vehicleId, Vehicle vehicle)
        {
            // TO DO: implement ...... vehicleId should be encoded some way

            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<IHttpActionResult> GetUserVehicles()
        {
            var username = this.User.Identity.Name;
            
            IEnumerable<Vehicle> vehicles = await _vehicleManager.GetVehiclesByUsernameAsync(username);

            return Ok(vehicles);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllVehicles()
        {
            // TO DO: some filtering parameters should be set....
            // in advanced scenario some complex filtering should be avaiable..... but this is in future

            await _vehicleManager.GetAllVehiclesPagedAsync();

            return Ok();
        }


        

        [HttpGet]
        public async Task<IHttpActionResult> GetVehicleTypes()
        {
            // this may be hardcoded to front end client .... otherwise it shoud be translated here .......
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetFuelTypes()
        {
            // this may be hardcoded to front end client .... otherwise it shoud be translated here .......
            return Ok();
        }
    }
}
