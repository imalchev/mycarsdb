using MyCarsDb.Business.Managers;
using MyCarsDb.Data.Contracts;
using MyCarsDb.Data.Contracts.Repositories;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCarsDb.UnitTests.Business.Managers.VehicleMakesManagerTests
{
    [TestFixture]
    public class GetAllModelsByMakeIdAsync_Should
    {
        [Test]
        public void Call_VehicleModelsRepository_GetVehicleModelsByMakeIdAsync()
        {
            // Arrange
            var vehicleModelsRepositoryMock = Substitute.For<IVehicleModelsDbRepository>();
            var myCarsDbDataStub = Substitute.For<IMyCarsDbData>();

            myCarsDbDataStub.VehicleModelsRepository.Returns(vehicleModelsRepositoryMock);

            var sut = new VehicleMakesManager(myCarsDbDataStub);

            int makeId = 5;

            // Act
            var modelsByMake = sut.GetAllModelsByMakeIdAsync(makeId);

            // Assert
            vehicleModelsRepositoryMock.Received().GetVehicleModelsByMakeIdAsync(Arg.Is(makeId));
        }
    }
}
