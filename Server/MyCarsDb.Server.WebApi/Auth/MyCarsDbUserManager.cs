namespace MyCarsDb.Server.WebApi.Auth
{    
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;

    using MyCarsDb.Data;    
    using MyCarsDb.Data.Models;

    public class MyCarsDbUserManager : UserManager<User, int>
    {
        private MyCarsDbUserManager(IUserStore<User, int> store)
            : base(store)
        {
            // Configure validation logic for usernames
            this.UserValidator = new UserValidator<User, int>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true,
            };

            // Configure validation logic for passwords
            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 3,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
        }

        public static MyCarsDbUserManager Create(IdentityFactoryOptions<MyCarsDbUserManager> options, IOwinContext context)
        {
            MyCarsDbContext dbContext = context.Get<MyCarsDbContext>();
            var userStore = new UserStore<User, Role, int, UserLogin, UserRole, UserClaim>(dbContext);
            var manager = new MyCarsDbUserManager(userStore);
            
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<User, int>(dataProtectionProvider.Create("Ala bala"));
            }

            return manager;
        }
    }
}