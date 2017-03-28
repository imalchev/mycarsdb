namespace MyCarsDb.Data.EntityFramework.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using MyCarsDb.Data.Contracts.Repositories;
    using MyCarsDb.Data.EntityFramework.Contracts;
    using MyCarsDb.Data.Models;

    public class UsersEfRepository : EfGenericRepository<User>, IUsersDbRepository
    {
        public UsersEfRepository(IMyCarsDbContext dbContext) 
            : base(dbContext)
        {
        }

        public async Task<User> FindByUserNameAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(nameof(username));
            }

            return await this.DbSet
                .Where(x => x.UserName == username)
                .FirstOrDefaultAsync();
        }

        public async Task<User> GetUserWithRolesAsync(int userId)
        {
            var userWithRoles = await this.DbContext.UsersToRoles
                .Where(x => x.UserId == userId)
                .Include(x => x.Role)
                .Select(x => x.User)
                .FirstOrDefaultAsync();

            return userWithRoles;
        }
    }
}
