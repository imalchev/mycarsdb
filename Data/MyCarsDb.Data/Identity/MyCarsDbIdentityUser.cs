namespace MyCarsDb.Data.Identity
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class MyCarsDbIdentityUser : IdentityUser<int, IdentityUserLogin<int>, IdentityUserRole<int>, IdentityUserClaim<int>>, IUser<int>
    {
    }    
}
