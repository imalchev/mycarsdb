namespace MyCarsDb.Data.EntityFramework.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    using MyCarsDb.Data.Models;

    internal class UserToRoleConfiguration : EntityTypeConfiguration<UserToRole>
    {
        public UserToRoleConfiguration()
        {
            this.ToTable("UsersToRoles");
            this.HasKey(x => new { x.UserId, x.RoleId });
        }
    }
}
