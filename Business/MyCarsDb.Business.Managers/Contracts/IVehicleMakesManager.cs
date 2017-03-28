
namespace MyCarsDb.Business.Managers.Contracts
{
    using MyCarsDb.Business.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IVehicleMakesManager
    {
        Task<IEnumerable<VehicleMake>> GetAllMakesAsync();
        Task<IEnumerable<VehicleModel>> GetAllModelsByMakeIdAsync(int makeId);
    }
}
