namespace MyCarsDb.Data.EntityFramework.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Linq;

    using MyCarsDb.Data.Contracts.Repositories;
    using MyCarsDb.Data.EntityFramework.Contracts;
    using MyCarsDb.Data.Models;    

    public class VehiclesEfRepository : EfGenericRepository<Vehicle>, IVehiclesDbRepository
    {
        public VehiclesEfRepository(IMyCarsDbContext dbContext) 
            : base(dbContext)
        {
        }

        public async Task<IList<Vehicle>> GetByUserAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Username can not be null or white space!", nameof(username));
            }

            var accessibleVehiclesQuery =
                from user in this.DbContext.Users
                join userToVehicle in this.DbContext.UsersToVehicles on user.Id equals userToVehicle.UserId
                join vehicle in this.DbContext.Vehicles on userToVehicle.UserId equals vehicle.Id
                where user.UserName == username
                select vehicle;

            return await accessibleVehiclesQuery
                .Include(x => x.UsersToVehicles)
                .ToListAsync();
        }
    }
}
