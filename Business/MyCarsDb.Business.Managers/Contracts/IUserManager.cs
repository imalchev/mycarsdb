namespace MyCarsDb.Business.Managers.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;

    using MyCarsDb.Data.Models;    

    public interface IUserManager
    {
        Task<IdentityResult> CreateAsync(User user, string password);

        Task<User> FindAsync(string userName, string password);

        Task<User> FindByEmailAsync(string email);

        Task<User> FindByIdAsync(int userId);

        Task<IList<string>> GetRolesAsync(int userId);
    }
}
