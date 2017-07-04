namespace MyCarsDb.Data.Contracts.Repositories
{    
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyCarsDb.Data.Models;

    public interface IVehicleModelsDbRepository : IDbGenericRepository<VehicleModel>
    {
        Task<IEnumerable<VehicleModel>> GetVehicleModelsByMakeIdAsync(int makeId);
    }
}
