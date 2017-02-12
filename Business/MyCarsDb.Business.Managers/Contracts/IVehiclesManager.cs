namespace MyCarsDb.Business.Managers.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyCarsDb.Business.Models;    

    public interface IVehiclesManager
    {
        Task AddNewVehicleAsync(Vehicle vehicle, string username);

        Task<IEnumerable<Vehicle>> GetVehiclesByUsernameAsync(string username);

        Task<IEnumerable<Vehicle>> GetAllVehiclesPagedAsync(int pageNumber = 1, int pageSize = 50);
    }
}
