namespace MyCarsDb.Data.Identity
{
    using Microsoft.AspNet.Identity.EntityFramework;

    public class MyCarsDbIdentityRole : IdentityRole<int, IdentityUserRole<int>>
    {
        public MyCarsDbIdentityRole()
        {
        }

        public MyCarsDbIdentityRole(string name)
        {
            this.Name = name;
        }
    }
}
