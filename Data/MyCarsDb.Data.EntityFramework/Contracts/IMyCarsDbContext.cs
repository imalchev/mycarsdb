namespace MyCarsDb.Data.EntityFramework.Contracts
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Threading;
    using System.Threading.Tasks;

    using MyCarsDb.Data.Models;
    using MyCarsDb.Data.Models.Contracts;

    public interface IMyCarsDbContext
    {
        IDbSet<UserLogin> UserLogins { get; set; }
        IDbSet<UserClaim> UserClaims { get; set; }
        IDbSet<UserToRole> UsersToRoles { get; set; }
        IDbSet<User> Users { get; set; }
        IDbSet<Role> Roles { get; set; }

        IDbSet<Vehicle> Vehicles { get; set; }
        IDbSet<UserToVehicle> UsersToVehicles { get; set; }        
        IDbSet<VehicleMake> VehicleMakes { get; set; }
        IDbSet<VehicleModel> VehicleModels { get; set; }        
        IDbSet<Event> Events { get; set; }
        IDbSet<Fueling> Fuelings { get; set; }
        IDbSet<FuelCalculation> FuelCalculations { get; set; }


        int SaveChanges();        
        Task<int> SaveChangesAsync();        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);               
        
        DbSet<TEntity> Set<TEntity>() where TEntity 
            : class, IEntity;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) 
            where TEntity : class, IEntity;
    }
}
