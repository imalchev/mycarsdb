namespace MyCarsDb.UnitTests.Business.Managers.IdentityManagerTests
{
    using System.Reflection;
    using System.Threading.Tasks;

    using NSubstitute;

    using NUnit.Framework;

    using MyCarsDb.Business.Managers;
    using MyCarsDb.Business.Managers.Contracts;
    using BM = MyCarsDb.Business.Models;
    using DM = MyCarsDb.Data.Models;    
    using MyCarsDb.Web.Infrastructure.Config;    

    [TestFixture]
    public class CreateAsync_Should
    {
        [OneTimeSetUp]
        public void Initialize()
        {
            var mappingsAssembly = Assembly.Load("MyCarsDb.Common.Mappings");
            AutoMapperConfig.Execute(mappingsAssembly);            
        }

        [Test]
        public async Task Call_UserManager_CreateAsync()
        {
            // Arrange
            var userManagerMock = Substitute.For<IUserManager>();

            IdentityManager identityManager = new IdentityManager(userManagerMock);

            var password = "1234";
            var registerUser = new BM.RegisterUser { Password = password };
            
            // Act
            await identityManager.CreateAsync(registerUser);

            // Assert
            await userManagerMock.Received().CreateAsync(Arg.Any<DM.User>(), Arg.Is(password));
        }
    }
}
