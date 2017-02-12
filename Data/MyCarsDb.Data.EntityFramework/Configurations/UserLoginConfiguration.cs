namespace MyCarsDb.Data.EntityFramework.Configurations
{    
    using System.Data.Entity.ModelConfiguration;

    using MyCarsDb.Data.Models;

    internal class UserLoginConfiguration : EntityTypeConfiguration<UserLogin>
    {
        public UserLoginConfiguration()
        {
            this.ToTable("Logins");
            this.HasKey(x => new { x.UserId, x.LoginProvider, x.ProviderKey });            
        }
    }
}
