namespace MyCarsDb.Business.Managers.Identity
{
    using Microsoft.AspNet.Identity;
    
    using MyCarsDb.Data.Models;

    public class RoleManager : RoleManager<Role, int>
    {
        public RoleManager(IRoleStore<Role, int> store) 
            : base(store)
        {
        }
    }
}
