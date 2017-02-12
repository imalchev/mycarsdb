namespace MyCarsDb.Business.Managers.Identity
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;

    using Microsoft.AspNet.Identity;

    using MyCarsDb.Data.Contracts;
    using MyCarsDb.Data.Models;    

    public class UserStore : 
        IUserStore<User, int>,
        IUserRoleStore<User, int>,
        IUserPasswordStore<User, int>, 
        IUserSecurityStampStore<User, int>,
        IDisposable
    {
        private readonly IMyCarsDbData _data;

        public UserStore(IMyCarsDbData data)
        {
            _data = data;
        }

        #region IUserStore
        public async Task CreateAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            
            _data.UsersRepository.Add(user);
            await _data.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            
            _data.UsersRepository.Remove(user);
            await _data.SaveChangesAsync();
        }       

        public async Task<User> FindByIdAsync(int userId)
        {
            return await _data.UsersRepository.FindByIdAsync(userId);
        }

        public async Task<User> FindByNameAsync(string userName)
        {
            return await _data.UsersRepository.FindByUserNameAsync(userName);
        }

        public async Task UpdateAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentException(nameof(user));
            }

            //var dbUser = await _data.UsersRepository.FindByIdAsync(user.Id);
            //if (dbUser == null)
            //{
            //    throw new ArgumentException("IdentityUser does not correspond to a User entity.", nameof(user));
            //}

            // populateUser(u, user);
            // dbUser.UserName = user.UserName

            _data.UsersRepository.Update(user);

            await _data.SaveChangesAsync();
        }
        #endregion

        #region IUserPasswordStore
        public Task<string> GetPasswordHashAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.PasswordHash);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult<bool>(!string.IsNullOrWhiteSpace(user.PasswordHash));
        }
        #endregion

        #region IUserSecurityStampStore
        public Task<string> GetSecurityStampAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.SecurityStamp);
        }

        public Task SetSecurityStampAsync(User user, string stamp)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.SecurityStamp = stamp;

            return Task.FromResult(0);
        }
        #endregion

        #region IUserRoleStore
        public async Task AddToRoleAsync(User user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("Role name can not be empty, white space or null", nameof(roleName));
            }

            var dbRole = await _data.RolesRepository.GetByNameAsync(roleName);
            if (dbRole == null)
            {
                throw new ArgumentException($"Role '{roleName}' does not exist in database", nameof(roleName));
            }

            var dbUser = await _data.UsersRepository.GetUserWithRolesAsync(user.Id);
            if (dbUser == null)
            {
                throw new ArgumentException($"User with Id '{user.Id}' does not exist in database", nameof(user));
            }

            if (dbUser.UsersToRoles.Where(x => x.Role.Name == roleName).Any())
            {
                throw new ArgumentException($"User with Id '{user.Id}' already contains  does not exist in database", nameof(user));
            }

            dbUser.UsersToRoles.Add(new UserToRole { Role = dbRole });

            _data.UsersRepository.Update(dbUser);
            await _data.SaveChangesAsync();
        }

        public async Task<IList<string>> GetRolesAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await _data.RolesRepository.GetByUserIdAsync(user.Id);
        }

        public async Task<bool> IsInRoleAsync(User user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("Argument cannot be null, empty, or whitespace: role.", nameof(roleName));
            }

            var dbUser = await _data.UsersRepository.GetUserWithRolesAsync(user.Id);
            if (dbUser == null)
            {
                throw new ArgumentException("User does not exist in database", nameof(user));
            }

            if (dbUser.UsersToRoles.Where(x => x.Role.Name == roleName).Any())
            {
                return true;
            }

            return false;
        }

        public async Task RemoveFromRoleAsync(User user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("Argument cannot be null, empty, or whitespace: role.", nameof(roleName));
            }

            var dbUser =  await _data.UsersRepository.GetUserWithRolesAsync(user.Id);
            if (dbUser == null)
            {
                throw new ArgumentException($"User with Id '{user.Id}' does not exist in database", nameof(user));
            }

            var userToRole = dbUser.UsersToRoles.FirstOrDefault(x => x.Role.Name == roleName);
            dbUser.UsersToRoles.Remove(userToRole);

            _data.UsersRepository.Update(dbUser);

            await _data.SaveChangesAsync();
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            // Do nothing - dependency inversion container should handle it
        }
        #endregion
    }
}
