namespace MyCarsDb.Server.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Linq;
    using System.Web.Http;

    using MyCarsDb.Data.Models;
    using MyCarsDb.Data.Models.Enums;
    using MyCarsDb.Server.WebApi.Controllers.Base;
    using MyCarsDb.Server.WebApi.Common;
    using MyCarsDb.Server.WebApi.DataTransferModels;

    [AllowAnonymous]
    public class VehiclesController : BaseController
    {
        private readonly List<VehicleTypeModel> vehicleTypes
            = Enum.GetValues(typeof(VehicleType))
               .Cast<VehicleType>()
               .Select(t => new VehicleTypeModel() { Id = (int)t, Name = t.ToString() }).ToList();

        private readonly List<FuelTypeModel> fuelTypes =
            Enum.GetValues(typeof(FuelType))
               .Cast<FuelType>()
               .Select(t => new FuelTypeModel() { Id = (int)t, Name = t.ToString() }).ToList();


        private IEnumerable<VehicleViewModel> ConvertFromVehicleToVehicleViewModel(IList<Vehicle> vehicles)
        {
            var result = new List<VehicleViewModel>(vehicles.Count);

            foreach (var vehicle in vehicles)
            {
                result.Add(new VehicleViewModel
                {
                    Id = Helper.EncodeId(vehicle.Id),
                    ModelName = vehicle.Model.ModelName,
                    MakeName = vehicle.Model.Make.Name,
                    EngineCapacity = vehicle.EngineCapacity,
                    ExactModel = vehicle.ExactModel,
                    FuelType = (short)vehicle.FuelTypes,
                    ManufactureDate = vehicle.ManufactureDate,
                    Power = vehicle.Power,
                    RegNumber = vehicle.RegNumber,
                    Type = vehicle.Type,
                    FuelTypesStr = this.ConvertFuelTypes(vehicle.FuelTypes)
                });
            }

            return result;
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

        private List<string> ConvertFuelTypes(FuelType fuelTypes)
        {
            var enumList = Enum.GetValues(typeof(FuelType)).OfType<FuelType>().ToList();
            var fuelTypesStr = new List<string>();
            foreach (var item in enumList)
            {
                if ((fuelTypes & item) > 0)
                {
                    fuelTypesStr.Add(Enum.GetName(typeof(FuelType), item));
                }
            }

            return fuelTypesStr;
        }

        private List<FuelType> GetFuelTypeList(FuelType fuelType)
        {
            var enumList = Enum.GetValues(typeof(FuelType)).OfType<FuelType>().ToList();
            var fuelTypes = new List<FuelType>();
            foreach (var type in enumList)
            {
                if (((short)fuelType & (short)type) > 0)
                {
                    fuelTypes.Add(type);
                }
            }

            return fuelTypes;
        }



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

        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> Edit(int vehicleId, DataTransferModels.VehicleModel model)
        {
            var vehicle = await this.DbContext.Vehicles.Where(x => x.Id == vehicleId).FirstOrDefaultAsync();

            vehicle.EngineCapacity = model.EngineCapacity;
            vehicle.ExactModel = model.ExactModel;
            vehicle.FuelTypes = CalculateFuelTpes(model.FuelTypes);
            vehicle.ManufactureDate = model.ManufactureDate;
            vehicle.ModelId = model.ModelId;
            vehicle.Power = model.Power;
            vehicle.RegNumber = model.RegNumber;
            vehicle.Type = model.Type;

            await this.DbContext.SaveChangesAsync();
            return Ok();
        }

        public async Task<IHttpActionResult> GetVehicleById(string id)
        {
            var vehicleId = Helper.DecodeId(id);

            var vehicle = await this.DbContext.Vehicles.Where(x => x.Id == vehicleId).FirstOrDefaultAsync();

            var model = new DataTransferModels.VehicleModel()
            {
                ManufactureDate = vehicle.ManufactureDate,
                EngineCapacity = vehicle.EngineCapacity,
                Power = vehicle.Power,
                ExactModel = vehicle.ExactModel,
                RegNumber = vehicle.RegNumber,
                ModelId = vehicle.ModelId,
                Type = vehicle.Type,   
                MakeId = vehicle.Model.MakeId                   
            };

            model.FuelTypes = this.GetFuelTypeList(vehicle.FuelTypes);
            return Ok(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IHttpActionResult> GetUserVehicles()
        {
            var userEmail = this.User.Identity.Name;
            var user = await this.UserManager.FindByEmailAsync(userEmail);

            var userVehicles = await this.DbContext.UsersToVehicles.Include(x => x.Vehicle.Model.Make)
                .Where(x => x.UserId == user.Id)
                .Select(x => x.Vehicle)
                .ToListAsync();

            return Ok(this.ConvertFromVehicleToVehicleViewModel(userVehicles));
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllVehicles()
        {
            var vehicles = await this.DbContext.Vehicles.Include(x => x.Model.Make).Select(x => x).ToListAsync();

            return Ok(this.ConvertFromVehicleToVehicleViewModel(vehicles));
        }


        [HttpGet]
        public List<VehicleMakeModel> GetMakes()
        {
            var makes = DbContext.VehicleMakes.ToList();
            var makesModel = makes.Select(x => new VehicleMakeModel() { Id = x.Id, Name = x.Name }).ToList();
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
