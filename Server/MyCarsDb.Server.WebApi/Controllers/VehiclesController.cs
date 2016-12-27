namespace MyCarsDb.Server.WebApi.Controllers
{
    using System.Web.Http;

    using MyCarsDb.Server.WebApi.Controllers.Base;
    using MyCarsDb.Server.WebApi.DataTransferModels;
    using Data.Models.Enums;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Net.Http;
    using Data.Models;

    [AllowAnonymous]
    public class VehiclesController : BaseController
    {
        private readonly Dictionary<int,string> vehicleTypes = Enum.GetValues(typeof(VehicleType))
               .Cast<VehicleType>()
               .ToDictionary(t => (int)t, t => t.ToString());

        private readonly Dictionary<int,string> fuelTypes =
            Enum.GetValues(typeof(FuelType))
               .Cast<FuelType>()
               .ToDictionary(t => (int)t, t => t.ToString());


        [HttpPost]
        public IHttpActionResult Add(DataTransferModels.VehicleModel model)
        {
            var vehicle = new Vehicle
            {
                EngineCapacity = model.EngineCapacity,
                FuelTypes = model.FuelTypes[0],//TODO: WTF
                ExactModel = model.ExactModel,
                RegNumber = "123",// изтрих го вече
                ManufactureDate = model.ManufactureDate,
                Power = model.Power,
                Type = model.Type
            };
            DbContext.Vehicles.Add(vehicle);
            DbContext.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public Dictionary<int, string> GetVehicleTypes()
        {
            return vehicleTypes;
        }

        [HttpGet]
        public Dictionary<int, string> GetFuelTypes()
        {
            return fuelTypes;
        }
    }
}
