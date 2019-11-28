using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.BanServiceTests
{
    [TestClass]
    public class CreateAsync_Should
    {
        [TestMethod]
        public async Task CreateInstanceOfTypeBan()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(CreateInstanceOfTypeBan));
            var mapperMock = new Mock<IDtoMapper<User, UserDto>>();
            var testGuid = Guid.NewGuid();
            var user = new User { Id = testGuid, UserName = "Pesho" };

            using (var actContext = new CWContext(options))
            {
                //Act
                //Guid id, string description, int period
                var sut = new BanService(actContext, mapperMock.Object);
                await actContext.Users.AddAsync(user);
                await actContext.SaveChangesAsync();
                await sut.CreateAsync(testGuid, "testDescription", 3);
                await actContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Assert
                var ban = assertContext.Bans.FirstOrDefault(b => b.User == user);
                Assert.IsInstanceOfType(ban, typeof(Ban));
            }
        }

        [TestMethod]
        public async Task SetCorrectInputValues()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(SetCorrectInputValues));
            var mapperMock = new Mock<IDtoMapper<User, UserDto>>();
            var testGuid = Guid.NewGuid();
            var user = new User { Id = testGuid, UserName = "Pesho" };

            using (var actContext = new CWContext(options))
            {
                //Act
                var sut = new BanService(actContext, mapperMock.Object);
                await actContext.Users.AddAsync(user);
                await actContext.SaveChangesAsync();
                await sut.CreateAsync(testGuid, "description", 3);
                await actContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Assert
                var ban = assertContext.Bans.Include(b => b.User).FirstOrDefault(b => b.User.UserName == "Pesho");
                Assert.AreEqual("Pesho", ban.User.UserName);
                Assert.AreEqual("description", ban.Description);
            }
        }
    }
}
