using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.BarServiceTests
{
    [TestClass]
    public class DeleteAsync_Should
    {
        [TestMethod]
        public async Task CorrectlyDeleteBar()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(CorrectlyDeleteBar));
            var mapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var searchMapperMock = new Mock<IDtoMapper<Bar, SearchBarDto>>();
            var cocktailMapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var guid = Guid.NewGuid();

            var entity = new Bar
            {
                Id = guid,
                Name = "testBar",
                Info = "testInfo",
                Address = "testAddress",
                ImagePath = "testImagePath",
                Phone = "111-333-666"
            };

            using (var actContext = new CWContext(options))
            {
                //Act
                await actContext.Bars.AddAsync(entity);
                await actContext.SaveChangesAsync();
                var service = new BarService(actContext, mapperMock.Object, searchMapperMock.Object, cocktailMapperMock.Object);
                var result = await service.DeleteAsync(guid);
                await actContext.SaveChangesAsync();
            }
            using (var assertContext = new CWContext(options))
            {
                //Assert
                var result = await assertContext.Bars.FirstAsync();
                Assert.AreEqual(true, result.IsDeleted);
            }
        }

        [TestMethod]
        public async Task ThrowWhen_NoBarFound()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ThrowWhen_NoBarFound));
            var mapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var searchMapperMock = new Mock<IDtoMapper<Bar, SearchBarDto>>();
            var cocktailMapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var guid = Guid.NewGuid();

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarService(assertContext, mapperMock.Object, searchMapperMock.Object, cocktailMapperMock.Object);
                await Assert.ThrowsExceptionAsync<BusinessLogicException>(() => sut.DeleteAsync(guid));
            }
        }

        [TestMethod]
        public async Task ReturnCorrectTypeOfInstance()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectTypeOfInstance));
            var mapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var searchMapperMock = new Mock<IDtoMapper<Bar, SearchBarDto>>();
            var cocktailMapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var guid = Guid.NewGuid();

            var entity = new Bar
            {
                Id = guid,
                Name = "testBar",
                Info = "testInfo",
                Address = "testAddress",
                ImagePath = "testImagePath",
                Phone = "111-333-666"
            };

            var dtoEntity = new BarDto
            {
                Id = guid,
                Name = "testBar",
                Info = "testInfo",
                Address = "testAddress",
                ImagePath = "testImagePath",
                Phone = "111-333-666"
            };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<Bar>())).Returns(dtoEntity);

            using (var actContext = new CWContext(options))
            {
                //Act
                await actContext.Bars.AddAsync(entity);
                await actContext.SaveChangesAsync();
            }
            using (var assertContext = new CWContext(options))
            {
                //Assert
                var sut = new BarService(assertContext, mapperMock.Object, searchMapperMock.Object, cocktailMapperMock.Object);
                var result = await sut.DeleteAsync(guid);
                Assert.IsInstanceOfType(result, typeof(BarDto));
                Assert.AreEqual(dtoEntity.Name, result.Name);
                Assert.AreEqual(dtoEntity.Info, result.Info);
                Assert.AreEqual(dtoEntity.Address, result.Address);
                Assert.AreEqual(dtoEntity.ImagePath, result.ImagePath);
                Assert.AreEqual(dtoEntity.Phone, result.Phone);
                Assert.AreEqual(dtoEntity.Id, result.Id);
            }
        }
    }
}
