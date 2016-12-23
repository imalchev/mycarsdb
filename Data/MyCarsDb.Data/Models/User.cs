namespace MyCarsDb.Data.Models
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser<int, UserLogin, UserRole, UserClaim>, IUser<int>
    {
    }
}
