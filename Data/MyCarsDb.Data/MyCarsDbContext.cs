namespace MyCarsDb.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;

    using Contracts;
    using Models;
    using System.Data.Entity;

    public class MyCarsDbContext : IdentityDbContext<User, Role, int, IdentityUserLogin<int>, IdentityUserRole<int>, IdentityUserClaim<int>>, IMyCarsDbContext
    {
        public MyCarsDbContext()
            : base("name=MyCarsDb")
        {            
        }

        public virtual IDbSet<Vehicle> Vehicles { get; set; }

        public virtual IDbSet<UserToVehicle> UsersToVehicles { get; set; }
    }
}