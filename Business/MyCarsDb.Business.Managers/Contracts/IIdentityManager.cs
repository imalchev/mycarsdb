namespace MyCarsDb.Business.Managers.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyCarsDb.Business.Models;    

    public interface IIdentityManager
    {        
        Task<User> FindAsync(string userName, string password);

        Task<User> FindByIdAsync(int userId);

        Task<User> FindByEmailAsync(string userEmail);

        Task<IdentityResult> CreateAsync(RegisterUser model);        

        Task<IdentityResult> AddToAdministratorRoleAsync(int userId, int addedByUserId);

        Task<ICollection<string>> GetRolesAsync(int userId);
    }
}
