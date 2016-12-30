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
        private readonly List<VehicleTypeModel> vehicleTypes 
            = Enum.GetValues(typeof(VehicleType))
               .Cast<VehicleType>()
               .Select(t => new VehicleTypeModel() { Id = (int)t,Name = t.ToString()}).ToList();

        private readonly List<FuelTypeModel> fuelTypes =
            Enum.GetValues(typeof(FuelType))
               .Cast<FuelType>()
               .Select(t => new FuelTypeModel() { Id = (int)t, Name = t.ToString() }).ToList();


        [HttpPost]
        public IHttpActionResult Add(DataTransferModels.VehicleModel model)
        {
            var vehicle = new Vehicle
            {
                EngineCapacity = model.EngineCapacity,
                FuelTypes = model.FuelTypes[0],//TODO: WTF
                ExactModel = model.ExactModel,
                RegNumber = model.RegNumber,
                ManufactureDate = model.ManufactureDate,
                Power = model.Power,
                Type = model.Type
            };
            DbContext.Vehicles.Add(vehicle);
            DbContext.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public List<VehicleMakeModel> GetMakes()
        {
            var makes = DbContext.VehicleMakes.ToList();
            var makesModel = makes.Select(x => new VehicleMakeModel() {Id=x.Id,Name=x.Name }).ToList();
            return makesModel;
        }

        [HttpGet]
        public List<VehicleModelModel> GetModelsByMakeId(int id)
        {
            var modelsDb = DbContext.VehicleModels.Where(x => x.MakeId == id).ToList();
            var models = modelsDb.Select(x => new VehicleModelModel() { Id = x.Id, Name = x.ModelName }).ToList();
            return models;
        }
        [HttpGet]
        public List<VehicleTypeModel> GetVehicleTypes()
        {
            return vehicleTypes;
        }

        [HttpGet]
        public List<FuelTypeModel> GetFuelTypes()

        {
            return fuelTypes;
        }
    }
}
