namespace MyCarsDb.Business.Managers.Identity
{    
    using Microsoft.AspNet.Identity;

    using MyCarsDb.Business.Managers.Contracts;
    using MyCarsDb.Data.Models;    

    public class UserManager : UserManager<User, int>, IUserManager
    {
        public UserManager(IUserStore<User, int> store, IUserTokenProvider<User, int> userTokenProvider) 
            : base(store)
        {
            this.UserTokenProvider = userTokenProvider;
        }
    }
}
