using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoMappers;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.BarServiceTests
{
    [TestClass]
    public class CreateAsync_Should
    {
        [TestMethod]
        public async Task CorrectlyCreateBar()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(CorrectlyCreateBar));
            var mapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var searchMapperMock = new Mock<IDtoMapper<Bar, SearchBarDto>>();
            var cocktailMapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();

            var entityDto = new BarDto
            {
                Id = Guid.NewGuid(),
                Name = "testBar",
                Info = "testInfo",
                Address = "testAddress",
                ImagePath = "testImagePath",
                Phone = "111-333-666"
            };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<Bar>())).Returns(entityDto);

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarService(assertContext, mapperMock.Object, searchMapperMock.Object, cocktailMapperMock.Object);
                var result = await sut.CreateAsync(entityDto);
                Assert.IsInstanceOfType(result, typeof(BarDto));
                Assert.AreEqual("testBar", result.Name);
                Assert.AreEqual("testInfo", result.Info);
                Assert.AreEqual("testAddress", result.Address);
                Assert.AreEqual("testImagePath", result.ImagePath);
                Assert.AreEqual("111-333-666", result.Phone);
                Assert.AreEqual(entityDto.Name, result.Name);
                Assert.AreEqual(entityDto.Info, result.Info);
                Assert.AreEqual(entityDto.Address, result.Address);
                Assert.AreEqual(entityDto.ImagePath, result.ImagePath);
                Assert.AreEqual(entityDto.Phone, result.Phone);
            }
        }

        [TestMethod]
        public async Task ThrowWhen_DtoPassedIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ThrowWhen_DtoPassedIsNull));
            var mapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var searchMapperMock = new Mock<IDtoMapper<Bar, SearchBarDto>>();
            var cocktailMapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            mapperMock.Setup(x => x.MapFrom(It.IsAny<Bar>())).Returns(It.IsAny<BarDto>);


            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarService(assertContext, mapperMock.Object, searchMapperMock.Object, cocktailMapperMock.Object);
                await Assert.ThrowsExceptionAsync<BusinessLogicException>(() => sut.CreateAsync(null));
            }
        }
    }
}
