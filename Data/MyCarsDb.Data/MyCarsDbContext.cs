namespace MyCarsDb.Data
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;    

    using Microsoft.AspNet.Identity.EntityFramework;

    using MyCarsDb.Data.Common.Attributes;
    using MyCarsDb.Data.Contracts;
    using MyCarsDb.Data.Models;    

    public class MyCarsDbContext : IdentityDbContext<User, Role, int, UserLogin, UserRole, UserClaim>, IMyCarsDbContext
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

            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<UserRole>().ToTable("UserRoles");
            modelBuilder.Entity<UserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<UserLogin>().ToTable("Logins");

            modelBuilder.Entity<Fueling>()
                .Property(c => c.EventId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
        
        public virtual IDbSet<Vehicle> Vehicles { get; set; }

        public virtual IDbSet<VehicleModel> VehicleModels { get; set; }

        public virtual IDbSet<UserToVehicle> UsersToVehicles { get; set; }

        public virtual IDbSet<Fueling> Fuelings { get; set; }

        public virtual IDbSet<FuelCalculation> FuelCalculations { get; set; }
    }
}