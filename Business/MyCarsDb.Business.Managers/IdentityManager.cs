namespace MyCarsDb.Business.Managers
{
    using System;
    using System.Collections.Generic;    
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;

    using BM = MyCarsDb.Business.Models;
    using MyCarsDb.Business.Managers.Contracts;
    using MyCarsDb.Business.Managers.Identity;
    using MyCarsDb.Common.Utility.Extensions;
    using DM = MyCarsDb.Data.Models;

    public class IdentityManager : IIdentityManager
    {
        private readonly IUserManager _userManager;

        public IdentityManager(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<BM.IdentityResult> AddToAdministratorRoleAsync(int userId, int addedByUserId)
        {
            throw new NotImplementedException();
        }

        public async Task<BM.IdentityResult> CreateAsync(BM.RegisterUser model)
        {
            DM.User user = model.MapTo<BM.RegisterUser, DM.User>();

            IdentityResult identityResult = await _userManager.CreateAsync(user, model.Password);

            return identityResult.MapTo<IdentityResult, BM.IdentityResult>();
        }

        public async Task<BM.User> FindAsync(string userName, string password)
        {
            DM.User user = await _userManager.FindAsync(userName, password);

            return user?.MapTo<DM.User, BM.User>();
        }

        public async Task<BM.User> FindByEmailAsync(string userEmail)
        {
            DM.User user = await _userManager.FindByEmailAsync(userEmail);

            return user?.MapTo<DM.User, BM.User>();
        }

        public async Task<BM.User> FindByIdAsync(int userId)
        {
            DM.User user = await _userManager.FindByIdAsync(userId);            

            return user?.MapTo<DM.User, BM.User>();
        }

        public async Task<ICollection<string>> GetRolesAsync(int userId)
        {
            return await _userManager.GetRolesAsync(userId);
        }
    }
}
