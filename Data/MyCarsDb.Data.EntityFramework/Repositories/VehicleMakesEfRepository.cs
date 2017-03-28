using MyCarsDb.Data.Contracts.Repositories;
using MyCarsDb.Data.EntityFramework.Contracts;
using MyCarsDb.Data.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MyCarsDb.Data.EntityFramework.Repositories
{
    public class VehicleMakesEfRepository : EfGenericRepository<VehicleMake>, IVehicleMakesDbRepository
    {
        public VehicleMakesEfRepository(IMyCarsDbContext dbContext) 
            : base(dbContext)
        {
        }

        public async Task<IList<VehicleMake>> GetAllMakesOrderdByNameAsync()
        {
            return await this.DbContext.VehicleMakes
                .OrderBy(x => x.Name)
                .ToListAsync();
        }
    }
}
