namespace MyCarsDb.Data.Contracts.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyCarsDb.Data.Models;    

    public interface IUsersDbRepository : IDbGenericRepository<User>
    {        
        Task<User> FindByUserNameAsync(string username);

        Task<User> GetUserWithRolesAsync(int userId);
    }
}
