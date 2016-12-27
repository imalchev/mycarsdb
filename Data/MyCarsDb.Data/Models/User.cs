namespace MyCarsDb.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;    

    public class User : IdentityUser<int, UserLogin, UserRole, UserClaim>, IUser<int>
    {
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
