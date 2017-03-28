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
        private IVehicleMakesDbRepository _vehicleMakesRepository;
        private IVehicleModelsDbRepository _vehicleModelsRepository;

        public MyCarsDbData(IMyCarsDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _dbContext = dbContext;
        }

        // TODO user some kind of factory instead of "new WhateverRepository()"
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

        public IVehicleMakesDbRepository VehicleMakesRepository
        {
            get
            {
                if (_vehiclesRepository == null)
                {
                    _vehicleMakesRepository = new VehicleMakesEfRepository(_dbContext);
                }

                return _vehicleMakesRepository;
            }
        }

        public IVehicleModelsDbRepository VehicleModelsRepository
        {
            get
            {
                if (_vehicleModelsRepository == null)
                {
                    _vehicleModelsRepository = new VehicleModelsEfRepository(_dbContext);
                }

                return _vehicleModelsRepository;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
