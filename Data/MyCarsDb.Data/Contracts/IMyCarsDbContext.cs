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
    }
}
