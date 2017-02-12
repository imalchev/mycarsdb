namespace MyCarsDb.Data.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyCarsDb.Data.Contracts;
    using MyCarsDb.Data.Contracts.Repositories;
    using MyCarsDb.Data.EntityFramework.Contracts;
    using MyCarsDb.Data.EntityFramework.Repositories;

    public class MyCarsDbData : IMyCarsDbData
    {
        private IMyCarsDbContext _dbContext;

        private IUsersDbRepository _usersDbRepository;
        private IRolesDbRepository _rolesDbRepository;
        private IVehiclesDbRepository _vehiclesRepository;

        public MyCarsDbData(IMyCarsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRolesDbRepository RolesRepository
        {
            get
            {
                if (_rolesDbRepository == null)
                {
                    _rolesDbRepository = new RolesEfRepository(_dbContext);
                }

                return _rolesDbRepository;
            }
        }

        public IUsersDbRepository UsersRepository
        {
            get
            {
                if (_usersDbRepository == null)
                {
                    _usersDbRepository = new UsersEfRepository(_dbContext);
                }

                return _usersDbRepository;
            }
        }

        public IVehiclesDbRepository VehiclesRepository
        {
            get
            {
                if (_vehiclesRepository == null)
                {
                    _vehiclesRepository = new VehiclesEfRepository(_dbContext);
                }

                return _vehiclesRepository;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
