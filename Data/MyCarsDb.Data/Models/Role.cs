namespace MyCarsDb.Data.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;    

    public class Role : IdentityRole<int, UserRole>
    {
        public Role()
        {
        }

        public Role(string name)
        {
            this.Name = name;
        }
    }
}
