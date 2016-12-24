namespace MyCarsDb.Data
{
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

        public virtual IDbSet<Vehicle> Vehicles { get; set; }

        public virtual IDbSet<UserToVehicle> UsersToVehicles { get; set; }

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

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}