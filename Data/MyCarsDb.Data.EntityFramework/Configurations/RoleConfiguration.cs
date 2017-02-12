namespace MyCarsDb.Data.EntityFramework.Configurations
{    
    using System.Data.Entity.ModelConfiguration;

    using MyCarsDb.Data.Models;

    public class RoleConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleConfiguration()
        {
            ToTable("Roles");

            //HasMany(x => x.Users)
            //    .WithMany(x => x.Roles)
            //    .Map(x =>
            //    {
            //        x.ToTable("UsersToRoles");
            //        x.MapLeftKey("RoleId");
            //        x.MapRightKey("UserId");
            //    });
        }
    }
}
