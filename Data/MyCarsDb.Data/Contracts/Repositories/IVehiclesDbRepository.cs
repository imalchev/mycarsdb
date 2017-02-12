namespace MyCarsDb.Data.Contracts.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyCarsDb.Data.Models;    

    public interface IVehiclesDbRepository : IDbGenericRepository<Vehicle>
    {
        Task<IList<Vehicle>> GetByUserAsync(string username);
    }
}
