namespace MyCarsDb.Business.Managers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using MyCarsDb.Business.Managers.Contracts;
    using BM = MyCarsDb.Business.Models;
    using MyCarsDb.Common.Utility.Extensions;
    using MyCarsDb.Data.Contracts;
    using DM = MyCarsDb.Data.Models;
    using System;

    public class VehicleMakesManager : IVehicleMakesManager
    {
        private IMyCarsDbData _data;

        public VehicleMakesManager(IMyCarsDbData data)
        {
            _data = data;
        }

        public async Task<IEnumerable<BM.VehicleMake>> GetAllMakesAsync()
        {
            IEnumerable<DM.VehicleMake> allMakes = await _data.VehicleMakesRepository
                .GetAllMakesOrderdByNameAsync();

            return allMakes.MapTo<DM.VehicleMake, BM.VehicleMake>();
        }

        public async Task<IEnumerable<BM.VehicleModel>> GetAllModelsByMakeIdAsync(int makeId)
        {
            IEnumerable<DM.VehicleModel> models = await _data.VehicleModelsRepository
                .GetVehicleModelsByMakeIdAsync(makeId);

            return models.MapTo<DM.VehicleModel, BM.VehicleModel>();
        }
    }
}
