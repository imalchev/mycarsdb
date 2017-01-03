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
    using System.Linq.Expressions;
    using Common;

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
        public async Task<IHttpActionResult> Edit(DataTransferModels.VehicleModel model)
        {
            var vehicle = await this.DbContext.Vehicles.Where(x => x.Id == model.VehicleId).FirstOrDefaultAsync();

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

        public async Task<DataTransferModels.VehicleModel> GetVehicleById(string id)
        {
            var vehicleId = Helper.DecodeId(id);

            var vehicle = await this.DbContext.Vehicles.Where(x => x.Id == vehicleId).FirstOrDefaultAsync();

            var model = new DataTransferModels.VehicleModel()
            { 
                VehicleId = vehicle.Id,
                ManufactureDate = vehicle.ManufactureDate,
                EngineCapacity = vehicle.EngineCapacity,
                Power = vehicle.Power,
                ExactModel = vehicle.ExactModel,
                RegNumber = vehicle.RegNumber,
                ModelId = vehicle.ModelId,
                Type = vehicle.Type,   
                MakeId = vehicle.Model.MakeId                   
            };

            model.FuelTypes = GetFuelTypeList(vehicle.FuelTypes);
            return model;

        }

        [HttpGet]
        [Authorize]
        public async Task<List<VehicleViewModel>> GetUserVehicles()
        {
            var userEmail = this.User.Identity.Name;
            var user = await this.UserManager.FindByEmailAsync(userEmail);
            var userVehicles = DbContext.UsersToVehicles.Include(x => x.Vehicle).Where(x => x.UserId == user.Id)
                .Select
                (x => new VehicleViewModel()
                {
                    VehicleId = x.VehicleId,
                    ModelName = x.Vehicle.Model.ModelName,
                    MakeName = x.Vehicle.Model.Make.Name,
                    EngineCapacity = x.Vehicle.EngineCapacity,
                    ExactModel = x.Vehicle.ExactModel,
                    FuelType = (short)x.Vehicle.FuelTypes,
                    ManufactureDate = x.Vehicle.ManufactureDate,
                    Power = x.Vehicle.Power,
                    RegNumber = x.Vehicle.RegNumber,
                    Type = x.Vehicle.Type
                })
                .ToList();

            foreach (var vehicle in userVehicles)
            {
                vehicle.FuelTypesStr = ConvertFuelTypes(vehicle.FuelType);
            }

            return userVehicles;
        }

        [HttpGet]
        public async Task<List<VehicleViewModel>> GetAllVehicles()
        {

            var vehicles = await DbContext.UsersToVehicles.Include(x => x.Vehicle)
                .Select
                (x => new VehicleViewModel()
                {
                    VehicleId = x.VehicleId,
                    ModelName = x.Vehicle.Model.ModelName,
                    MakeName = x.Vehicle.Model.Make.Name,
                    EngineCapacity = x.Vehicle.EngineCapacity,
                    ExactModel = x.Vehicle.ExactModel,
                    FuelType = (short)x.Vehicle.FuelTypes,
                    ManufactureDate = x.Vehicle.ManufactureDate,
                    Power = x.Vehicle.Power,
                    RegNumber = x.Vehicle.RegNumber,
                    Type = x.Vehicle.Type,
                   
                   
                })
                .ToListAsync();

            foreach (var vehicle in vehicles)
            {
                vehicle.FuelTypesStr = ConvertFuelTypes(vehicle.FuelType);
            }

            return vehicles;
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

        private List<string> ConvertFuelTypes(short fueltypes)
        {
            var enumList = Enum.GetValues(typeof(FuelType)).OfType<FuelType>().ToList();
            var fuelTypesStr = new List<string>();
            foreach (var item in enumList)
            {
                if ((fueltypes & (short)item) > 0)
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
                if (((short)fuelType & (short)type)>0)
                {
                    fuelTypes.Add(type);
                }
            }

            return fuelTypes;
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
