using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.BarServiceTests
{
    [TestClass]
    public class RemoveCocktailsAsync_Should
    {
        [TestMethod]
        public async Task CorrectlyRemoveCocktailsWhen_ParamsAreValid()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(CorrectlyRemoveCocktailsWhen_ParamsAreValid));
            var mapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var searchMapperMock = new Mock<IDtoMapper<Bar, SearchBarDto>>();
            var cocktailMapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var testGuid = Guid.NewGuid();
            var cocktailGuid = Guid.NewGuid();

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

            var list = new List<string> { "Boza" };

            var cocktail = new Cocktail { Id = cocktailGuid, Name = "Boza" };
            var barCocktail = new BarCocktail { BarId = bar1.Id, CocktailId = cocktailGuid };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<Bar>())).Returns(barDto1);

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Bars.AddAsync(bar1);
                await arrangeContext.Cocktails.AddAsync(cocktail);
                await arrangeContext.BarCocktails.AddAsync(barCocktail);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarService(assertContext, mapperMock.Object, searchMapperMock.Object, cocktailMapperMock.Object);
                var result = await sut.RemoveCocktailsAsync(barDto1, list);
                var barCocktailEntity = await assertContext.BarCocktails.FirstAsync();
                Assert.IsInstanceOfType(result, typeof(BarDto));
                Assert.AreEqual("testBar1", result.Name);
                Assert.AreEqual(true, barCocktailEntity.IsDeleted);
            }
        }

        [TestMethod]
        public async Task ThrowWhen_IngredientNotFound()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ThrowWhen_IngredientNotFound));
            var mapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var searchMapperMock = new Mock<IDtoMapper<Bar, SearchBarDto>>();
            var cocktailMapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var testGuid = Guid.NewGuid();
            var cocktailGuid = Guid.NewGuid();

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

            var list = new List<string> { "Banica" };

            var cocktail = new Cocktail { Id = cocktailGuid, Name = "Boza" };
            var barCocktail = new BarCocktail { BarId = bar1.Id, CocktailId = cocktailGuid };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<Bar>())).Returns(barDto1);

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Bars.AddAsync(bar1);
                await arrangeContext.Cocktails.AddAsync(cocktail);
                await arrangeContext.BarCocktails.AddAsync(barCocktail);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarService(assertContext, mapperMock.Object, searchMapperMock.Object, cocktailMapperMock.Object);
                await Assert.ThrowsExceptionAsync<BusinessLogicException>(() => sut.RemoveCocktailsAsync(barDto1, list));
            }
        }

        [TestMethod]
        public async Task ThrowWhen_BarCocktailNotFound()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ThrowWhen_BarCocktailNotFound));
            var mapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var searchMapperMock = new Mock<IDtoMapper<Bar, SearchBarDto>>();
            var cocktailMapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var testGuid = Guid.NewGuid();
            var cocktailGuid = Guid.NewGuid();

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

            var list = new List<string> { "Boza" };

            var cocktail = new Cocktail { Id = cocktailGuid, Name = "Boza" };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<Bar>())).Returns(barDto1);

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Bars.AddAsync(bar1);
                await arrangeContext.Cocktails.AddAsync(cocktail);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarService(assertContext, mapperMock.Object, searchMapperMock.Object, cocktailMapperMock.Object);
                await Assert.ThrowsExceptionAsync<BusinessLogicException>(() => sut.RemoveCocktailsAsync(barDto1, list));
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
            var cocktailGuid = Guid.NewGuid();

            var barDto1 = new BarDto
            {
                Id = testGuid,
                Name = "testBar1",
            };

            var list = new List<string> { "Boza" };

            var cocktail = new Cocktail { Id = cocktailGuid, Name = "Boza" };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<Bar>())).Returns(barDto1);

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Cocktails.AddAsync(cocktail);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarService(assertContext, mapperMock.Object, searchMapperMock.Object, cocktailMapperMock.Object);
                await Assert.ThrowsExceptionAsync<BusinessLogicException>(() => sut.RemoveCocktailsAsync(barDto1, list));
            }
        }

        [TestMethod]
        public async Task ThrowWhen_InputListIsEmpty()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ThrowWhen_InputListIsEmpty));
            var mapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var searchMapperMock = new Mock<IDtoMapper<Bar, SearchBarDto>>();
            var cocktailMapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var testGuid = Guid.NewGuid();
            var cocktailGuid = Guid.NewGuid();

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

            var list = new List<string>();

            var cocktail = new Cocktail { Id = cocktailGuid, Name = "Boza" };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<Bar>())).Returns(barDto1);

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Bars.AddAsync(bar1);
                await arrangeContext.Cocktails.AddAsync(cocktail);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarService(assertContext, mapperMock.Object, searchMapperMock.Object, cocktailMapperMock.Object);
                await Assert.ThrowsExceptionAsync<BusinessLogicException>(() => sut.RemoveCocktailsAsync(barDto1, list));
            }
        }
    }
}
