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

    public class RolesEfRepository : EfGenericRepository<Role>, IRolesDbRepository
    {
        public RolesEfRepository(IMyCarsDbContext dbContext) 
            : base(dbContext)
        {
        }

        public async Task<Role> GetByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return await this.DbSet
                .Where(x => x.Name == name)
                .SingleOrDefaultAsync();
        }

        public async Task<IList<string>> GetByUserIdAsync(int userId)
        {
            return await this.DbContext.UsersToRoles
                .Where(x => x.UserId == userId)
                .Select(x => x.Role.Name)
                .ToListAsync();
        }
    }
}
