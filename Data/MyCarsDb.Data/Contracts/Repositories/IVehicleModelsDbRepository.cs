using MyCarsDb.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCarsDb.Data.Contracts.Repositories
{
    public interface IVehicleModelsDbRepository : IDbGenericRepository<VehicleModel>
    {
        Task<IEnumerable<VehicleModel>> GetVehicleModelsByMakeIdAsync(int makeId);
    }
}
