namespace MyCarsDb.Data.Contracts
{
    using System.Data;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Threading.Tasks;

    using MyCarsDb.Data.Models;

    public interface IMyCarsDbContext
    {
        IDbSet<User> Users { get; set; }
        IDbSet<Role> Roles { get; set; }

        IDbSet<Vehicle> Vehicles { get; set; }
        IDbSet<UserToVehicle> UsersToVehicles { get; set; }        
        IDbSet<VehicleMake> VehicleMakes { get; set; }
        IDbSet<VehicleModel> VehicleModels { get; set; }        
        IDbSet<Event> Events { get; set; }
        IDbSet<Fueling> Fuelings { get; set; }
        IDbSet<FuelCalculation> FuelCalculations { get; set; }
    }
}
