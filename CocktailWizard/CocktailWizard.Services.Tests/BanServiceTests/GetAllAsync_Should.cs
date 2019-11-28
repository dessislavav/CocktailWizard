using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.BanServiceTests
{
    [TestClass]
    public class GetAllAsync_Should
    {
        [TestMethod]
        public async Task ReturnCorrectTypeOfInstance_WhenParamIsActive()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectTypeOfInstance_WhenParamIsActive));
            var mapperMock = new Mock<IDtoMapper<User, UserDto>>();
            var testGuid = Guid.NewGuid();
            var testGuid2 = Guid.NewGuid();

            var users = new List<User>
            {
                new User { Id = testGuid, UserName = "Pesho", IsBanned = false },
                new User { Id = testGuid2, UserName = "Ivan", IsBanned = false },
            };

            var userDtos = new List<UserDto>
            {
                new UserDto { Id = testGuid, UserName = "Pesho", IsBanned = false },
                new UserDto { Id = testGuid2, UserName = "Ivan", IsBanned = false },
            };


            mapperMock.Setup(u => u.MapFrom(It.IsAny<ICollection<User>>())).Returns(userDtos);

            using (var actContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BanService(actContext, mapperMock.Object);
                await actContext.Users.AddAsync(users[0]);
                await actContext.Users.AddAsync(users[1]);
                await actContext.SaveChangesAsync();

                var result = await sut.GetAllAsync("active");
                Assert.IsInstanceOfType(result, typeof(ICollection<UserDto>));
            }
        }

        [TestMethod]
        public async Task ReturnCorrectObjects_WhenParamIsActive()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectObjects_WhenParamIsActive));
            var mapperMock = new Mock<IDtoMapper<User, UserDto>>();
            var testGuid = Guid.NewGuid();
            var testGuid2 = Guid.NewGuid();

            var users = new List<User>
            {
                new User { Id = testGuid, UserName = "Pesho", IsBanned = false },
                new User { Id = testGuid2, UserName = "Ivan", IsBanned = false },
            };

            var userDtos = new List<UserDto>
            {
                new UserDto { Id = testGuid, UserName = "Pesho", IsBanned = false },
                new UserDto { Id = testGuid2, UserName = "Ivan", IsBanned = false },
            };


            mapperMock.Setup(u => u.MapFrom(It.IsAny<ICollection<User>>())).Returns(userDtos);

            using (var actContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BanService(actContext, mapperMock.Object);
                await actContext.Users.AddAsync(users[0]);
                await actContext.Users.AddAsync(users[1]);
                await actContext.SaveChangesAsync();

                var result = await sut.GetAllAsync("active");
                Assert.AreEqual(users[0].UserName, result.AsQueryable().First().UserName);
                Assert.AreEqual(users[0].Id, result.AsQueryable().First().Id);
                Assert.AreEqual(users[0].IsBanned, result.AsQueryable().First().IsBanned);
                Assert.AreEqual(users[1].UserName, result.AsQueryable().Last().UserName);
                Assert.AreEqual(users[1].Id, result.AsQueryable().Last().Id);
                Assert.AreEqual(users[1].IsBanned, result.AsQueryable().Last().IsBanned);
            }
        }

        [TestMethod]
        public async Task ReturnCorrectTypeOfInstance_WhenParamIsBanned()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectTypeOfInstance_WhenParamIsBanned));
            var mapperMock = new Mock<IDtoMapper<User, UserDto>>();
            var testGuid = Guid.NewGuid();
            var testGuid2 = Guid.NewGuid();

            var users = new List<User>
            {
                new User { Id = testGuid, UserName = "Pesho", IsBanned = true },
                new User { Id = testGuid2, UserName = "Ivan", IsBanned = true },
            };

            var userDtos = new List<UserDto>
            {
                new UserDto { Id = testGuid, UserName = "Pesho", IsBanned = true },
                new UserDto { Id = testGuid2, UserName = "Ivan", IsBanned = true },
            };


            mapperMock.Setup(u => u.MapFrom(It.IsAny<ICollection<User>>())).Returns(userDtos);

            using (var actContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BanService(actContext, mapperMock.Object);
                await actContext.Users.AddAsync(users[0]);
                await actContext.Users.AddAsync(users[1]);
                await actContext.SaveChangesAsync();

                var result = await sut.GetAllAsync("banned");
                Assert.IsInstanceOfType(result, typeof(ICollection<UserDto>));
            }
        }

        [TestMethod]
        public async Task ReturnCorrectObjects_WhenParamIsBanned()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectObjects_WhenParamIsBanned));
            var mapperMock = new Mock<IDtoMapper<User, UserDto>>();
            var testGuid = Guid.NewGuid();
            var testGuid2 = Guid.NewGuid();

            var users = new List<User>
            {
                new User { Id = testGuid, UserName = "Pesho", IsBanned = true },
                new User { Id = testGuid2, UserName = "Ivan", IsBanned = true },
            };

            var userDtos = new List<UserDto>
            {
                new UserDto { Id = testGuid, UserName = "Pesho", IsBanned = true },
                new UserDto { Id = testGuid2, UserName = "Ivan", IsBanned = true },
            };


            mapperMock.Setup(u => u.MapFrom(It.IsAny<ICollection<User>>())).Returns(userDtos);

            using (var actContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BanService(actContext, mapperMock.Object);
                await actContext.Users.AddAsync(users[0]);
                await actContext.Users.AddAsync(users[1]);
                await actContext.SaveChangesAsync();

                var result = await sut.GetAllAsync("banned");
                Assert.AreEqual(users[0].UserName, result.AsQueryable().First().UserName);
                Assert.AreEqual(users[0].Id, result.AsQueryable().First().Id);
                Assert.AreEqual(users[0].IsBanned, result.AsQueryable().First().IsBanned);
                Assert.AreEqual(users[1].UserName, result.AsQueryable().Last().UserName);
                Assert.AreEqual(users[1].Id, result.AsQueryable().Last().Id);
                Assert.AreEqual(users[1].IsBanned, result.AsQueryable().Last().IsBanned);
            }
        }
    }
}
