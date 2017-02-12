namespace MyCarsDb.Data.Contracts.Repositories
{    
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyCarsDb.Data.Models;

    public interface IRolesDbRepository : IDbGenericRepository<Role>
    {
        Task<Role> GetByNameAsync(string name);

        Task<IList<string>> GetByUserIdAsync(int userId);
    }
}
