namespace MyCarsDb.Data.EntityFramework.Configurations
{    
    using System.Data.Entity.ModelConfiguration;

    using MyCarsDb.Data.Models;

    public class UserClaimConfiguration : EntityTypeConfiguration<UserClaim>
    {
        public UserClaimConfiguration()
        {
            ToTable("UserClaims");
        }
    }
}
