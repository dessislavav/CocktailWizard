using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.CustomExceptions;
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
    public class GetBarCocktailsAsync_Should
    {
        [TestMethod]
        public async Task ReturnCorrectDtoWhen_ParamsAreValid()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectDtoWhen_ParamsAreValid));
            var mapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var searchMapperMock = new Mock<IDtoMapper<Bar, SearchBarDto>>();
            var cocktailMapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var testGuid = Guid.NewGuid();

            var bar1 = new Bar
            {
                Id = testGuid,
                Name = "testBar1",
            };

            var barDto1 = new BarDto
            {
                Id = testGuid,
                Name = "testBar1",
            };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<Bar>())).Returns(barDto1);

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Bars.AddAsync(bar1);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarService(assertContext, mapperMock.Object, searchMapperMock.Object, cocktailMapperMock.Object);
                var result = await sut.GetBarCocktailsAsync(testGuid);
                Assert.IsInstanceOfType(result, typeof(BarDto));
                Assert.AreEqual("testBar1", result.Name);
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
            var testGuid = Guid.NewGuid();

            mapperMock.Setup(x => x.MapFrom(It.IsAny<Bar>())).Returns(It.IsAny<BarDto>());

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarService(assertContext, mapperMock.Object, searchMapperMock.Object, cocktailMapperMock.Object);
                await Assert.ThrowsExceptionAsync<BusinessLogicException>(() => sut.GetBarCocktailsAsync(testGuid));
            }
        }
    }
}
