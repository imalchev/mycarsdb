namespace MyCarsDb.UnitTests.Business.Managers.VehicleMakesManagerTests
{
    using System.Threading.Tasks;

    using NSubstitute;

    using NUnit.Framework;

    using MyCarsDb.Business.Managers;
    using MyCarsDb.Data.Contracts;
    using MyCarsDb.Data.Contracts.Repositories;

    [TestFixture]
    public class GetAllMakesAsync_Should
    {
        [Test]
        public void Call_VehicleMakesRepository_GetAllAsync()
        {
            // Arrange
            var vehicleMakesRepositoryMock = Substitute.For<IVehicleMakesDbRepository>();
            var myCarsDbDataStub = Substitute.For<IMyCarsDbData>();

            myCarsDbDataStub.VehicleMakesRepository.Returns(vehicleMakesRepositoryMock);

            var sut = new VehicleMakesManager(myCarsDbDataStub);

            // Act
            var allMakes = sut.GetAllMakesAsync();

            // Assert
            vehicleMakesRepositoryMock.Received().GetAllMakesOrderdByNameAsync();
        }
    }
}
