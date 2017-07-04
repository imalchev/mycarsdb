namespace MyCarsDb.Data.Contracts.Repositories
{    
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyCarsDb.Data.Models;

    public interface IVehicleMakesDbRepository : IDbGenericRepository<VehicleMake>
    {
        Task<IList<VehicleMake>> GetAllMakesOrderdByNameAsync();
    }
}
