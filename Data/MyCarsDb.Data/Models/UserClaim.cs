namespace MyCarsDb.Data.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.ComponentModel.DataAnnotations;

    public class UserClaim : IdentityUserClaim<int>
    {
        [MaxLength(32)]
        public override string ClaimType { get; set; }

        [MaxLength(128)]
        public override string ClaimValue { get; set; }    
    }
}
