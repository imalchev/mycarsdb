namespace MyCarsDb.Server.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using System.Web.Http;

    using MyCarsDb.Data.Models;
    using MyCarsDb.Data.Models.Enums;
    using MyCarsDb.Server.WebApi.Auth;
    using MyCarsDb.Server.WebApi.Controllers.Base;
    using MyCarsDb.Server.WebApi.DataTransferModels;
    using System.Data.Entity;

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

        public VehiclesController()
        {
        }


        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> Add(DataTransferModels.VehicleModel model)
        {
            var userEmail = this.User.Identity.Name;
            var user = await this.UserManager.FindByEmailAsync(userEmail);

            var vehicle = new Vehicle
            {
                ModelId = model.ModelId,
                EngineCapacity = model.EngineCapacity,
                FuelTypes = this.CalculateFuelTpes(model.FuelTypes),
                ExactModel = model.ExactModel,
                RegNumber = model.RegNumber,
                ManufactureDate = new DateTime(model.ManufactureDate.Year, model.ManufactureDate.Month, 1), // always set first day of the month
                Power = model.Power,
                Type = model.Type
            };

            this.DbContext.Vehicles.Add(vehicle);

            var userToVehicle = new UserToVehicle
            {
                User = user,
                Vehicle = vehicle,
                AccessType = UserToVehicleAccessType.Administratior
            };
            this.DbContext.UsersToVehicles.Add(userToVehicle);

            await this.DbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<List<VehicleViewModel>> GetUserVehicles()
        {
            var userEmail = this.User.Identity.Name;
            var user = await this.UserManager.FindByEmailAsync(userEmail);
            var userVehicles = DbContext.UsersToVehicles.Include(x=>x.Vehicle).Where(x => x.UserId == user.Id)
                .Select
                (x => new VehicleViewModel()
                {
                    ModelName = x.Vehicle.Model.ModelName,
                    MakeName = x.Vehicle.Model.Make.Name,
                    EngineCapacity = x.Vehicle.EngineCapacity,
                    ExactModel = x.Vehicle.ExactModel,
                    FuelType = (short)x.Vehicle.FuelTypes,
                    ManufactureDate=x.Vehicle.ManufactureDate,
                    Power = x.Vehicle.Power,
                    RegNumber = x.Vehicle.RegNumber,
                    Type = x.Vehicle.Type
                } )
                .ToList();

            foreach (var vehicle in userVehicles)
            {
                vehicle.FuelTypesStr = ConvertFuelTypes(vehicle.FuelType);
            }

            return userVehicles;
        }

        private FuelType CalculateFuelTpes(IEnumerable<FuelType> fuelTypes)
        {
            FuelType result = (FuelType)0;
            foreach (FuelType type in fuelTypes)
            {
                result = result | type;
            }

            return result;
        }

        private List<string> ConvertFuelTypes (short fueltypes)
        {
            var enumList = Enum.GetValues(typeof(FuelType)).OfType<FuelType>().ToList();
            var fuelTypesStr = new List<string>();
            foreach (var item in enumList)
            {
                if((fueltypes & (short)item)>0)
                {
                    fuelTypesStr.Add(Enum.GetName(typeof(FuelType), item));
                }
            }

            return fuelTypesStr;
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
