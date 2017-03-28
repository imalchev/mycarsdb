using MyCarsDb.Data.Contracts.Repositories;
using MyCarsDb.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCarsDb.Data.EntityFramework.Contracts;
using System.Data.Entity;

namespace MyCarsDb.Data.EntityFramework.Repositories
{
    public class VehicleModelsEfRepository : EfGenericRepository<VehicleModel>, IVehicleModelsDbRepository
    {
        public VehicleModelsEfRepository(IMyCarsDbContext dbContext) 
            : base(dbContext)
        {
        }

        public async Task<IEnumerable<VehicleModel>> GetVehicleModelsByMakeIdAsync(int makeId)
        {
            return await base.DbContext.VehicleModels
                .Where(x => x.MakeId == makeId)
                .ToListAsync();
        }
    }
}
