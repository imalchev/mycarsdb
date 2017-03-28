namespace MyCarsDb.Business.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;

    using BM = MyCarsDb.Business.Models;
    using MyCarsDb.Business.Managers.Contracts;
    using MyCarsDb.Common.Utility.Extensions;
    using MyCarsDb.Data.Contracts;
    using DM = MyCarsDb.Data.Models;    

    public class VehiclesManager : IVehiclesManager
    {
        private readonly IMyCarsDbData _data;

        public VehiclesManager(IMyCarsDbData data)
        {
            _data = data;
        }

        public async Task AddNewVehicleAsync(BM.Vehicle vehicleBO, string username)
        {
            if (vehicleBO == null)
            {
                throw new ArgumentNullException(nameof(vehicleBO));
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Username can not be null or white space.", nameof(username));
            }

            DM.Vehicle vehicle = vehicleBO.MapTo<BM.Vehicle, DM.Vehicle>();

            DM.User user = await _data.UsersRepository.FindByUserNameAsync(username);
            if (user == null)
            {
                throw new ArgumentException($"Username {username} does not exist!", nameof(user));
            }

            var userToVehicle = new DM.UserToVehicle
            {
                User = user,
                Vehicle = vehicle,
                AccessType = DM.Enums.UserToVehicleAccessType.Administratior
            };

            vehicle.UsersToVehicles.Add(userToVehicle);

            _data.VehiclesRepository.Add(vehicle);

            await _data.SaveChangesAsync();
        }

        public Task<IEnumerable<BM.Vehicle>> GetAllVehiclesPagedAsync(int pageNumber = 1, int pageSize = 50)
        {
            if (pageNumber <= 0)
            {
                throw new ArgumentOutOfRangeException("Only positive page numbers are awolled", nameof(pageNumber));
            }

            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException("Only positive page sizes are awolled", nameof(pageSize));
            }

            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BM.Vehicle>> GetVehiclesByUsernameAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Username can not be null or white space.", nameof(username));
            }

            IList<DM.Vehicle> vehicles = await _data.VehiclesRepository.GetByUserAsync(username);

            return vehicles.Select(x => x.MapTo<DM.Vehicle, BM.Vehicle>());
        }
    }
}
