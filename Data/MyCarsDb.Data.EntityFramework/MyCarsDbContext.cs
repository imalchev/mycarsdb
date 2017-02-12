namespace MyCarsDb.Data.EntityFramework
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using MyCarsDb.Data.EntityFramework.Common.AttributeConventions;
    using MyCarsDb.Data.EntityFramework.Configurations;
    using MyCarsDb.Data.EntityFramework.Contracts;
    using MyCarsDb.Data.Models;
    using MyCarsDb.Data.Models.Contracts;

    public class MyCarsDbContext : DbContext, IMyCarsDbContext
    {
        public MyCarsDbContext()
            : base("name=MyCarsDb")
        {
        }

        public static MyCarsDbContext Create()
        {
            return new MyCarsDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(new DecimalAttributeConvention());
            modelBuilder.Conventions.Add(new TimeAttributeConvention());

            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new UserToRoleConfiguration());
            modelBuilder.Configurations.Add(new UserLoginConfiguration());
            modelBuilder.Configurations.Add(new UserClaimConfiguration());

            modelBuilder.Configurations.Add(new EventConfiguration());
            modelBuilder.Configurations.Add(new FuelingConfiguration());
        }

        public new DbSet<TEntity> Set<TEntity>()
            where TEntity : class, IEntity
        {
            return base.Set<TEntity>();
        }

        public new DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            return base.Entry<TEntity>(entity);
        }


        public virtual IDbSet<User> Users { get; set; }
        public virtual IDbSet<Role> Roles { get; set; }
        public virtual IDbSet<UserToRole> UsersToRoles { get; set; }
        public virtual IDbSet<UserLogin> UserLogins { get; set; }
        public virtual IDbSet<UserClaim> UserClaims { get; set; }

        public virtual IDbSet<Vehicle> Vehicles { get; set; }
        public virtual IDbSet<VehicleMake> VehicleMakes { get; set; }
        public virtual IDbSet<VehicleModel> VehicleModels { get; set; }
        public virtual IDbSet<UserToVehicle> UsersToVehicles { get; set; }
        public virtual IDbSet<Event> Events { get; set; }
        public virtual IDbSet<Fueling> Fuelings { get; set; }
        public virtual IDbSet<FuelCalculation> FuelCalculations { get; set; }        
    }
}