using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoMappers;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.BarServiceTests
{
    [TestClass]
    public class AddCocktailsAsync_Should
    {
        [TestMethod]
        public async Task CorrectlyAddCocktailsToBar()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(CorrectlyAddCocktailsToBar));
            var mapper = new BarDtoMapper();

            var mapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var searchMapperMock = new Mock<IDtoMapper<Bar, SearchBarDto>>();
            var cocktailMapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();

            var guid = Guid.NewGuid();

            var entityDto = new BarDto
            {
                Id = guid,
                Name = "testBar",
                Info = "testInfo",
                Address = "testAddress",
                ImagePath = "testImagePath",
                Phone = "111-333-666"
            };

            var bar = new Bar
            {
                Id = guid,
                Name = "testBar",
                Info = "testInfo",
                Address = "testAddress",
                ImagePath = "testImagePath",
                Phone = "111-333-666"
            };

            var cocktail = new Cocktail
            {
                Id = Guid.NewGuid(),
                Name = "testCocktail",
                Info = "testCocktailInfo",
            };

            var list = new List<string>() { "testCocktail" };

            using (var actContext = new CWContext(options))
            {
                //Act
                await actContext.Bars.AddAsync(bar);
                await actContext.Cocktails.AddAsync(cocktail);
                await actContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarService(assertContext, mapperMock.Object, searchMapperMock.Object, cocktailMapperMock.Object);
                var result = await sut.AddCocktailsAsync(entityDto, list);
                Assert.AreEqual(1, await assertContext.BarCocktails.CountAsync());
            }
        }

        [TestMethod]
        public async Task ReturnCorectTypeOfInstance()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorectTypeOfInstance));
            var mapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var searchMapperMock = new Mock<IDtoMapper<Bar, SearchBarDto>>();
            var cocktailMapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var guid = Guid.NewGuid();

            var entityDto = new BarDto
            {
                Id = guid,
                Name = "testBar",
                Info = "testInfo",
                Address = "testAddress",
                ImagePath = "testImagePath",
                Phone = "111-333-666"
            };

            var bar = new Bar
            {
                Id = guid,
                Name = "testBar",
                Info = "testInfo",
                Address = "testAddress",
                ImagePath = "testImagePath",
                Phone = "111-333-666"
            };

            var cocktail = new Cocktail
            {
                Id = Guid.NewGuid(),
                Name = "testCocktail",
                Info = "testCocktailInfo",
            };

            var list = new List<string>() { "testCocktail" };

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Bars.AddAsync(bar);
                await arrangeContext.Cocktails.AddAsync(cocktail);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarService(assertContext, mapperMock.Object, searchMapperMock.Object, cocktailMapperMock.Object);
                var result = await sut.AddCocktailsAsync(entityDto, list);
                Assert.IsInstanceOfType(result, typeof(BarDto));
            }
        }

        [TestMethod]
        public async Task CreateBarCocktailWithCorrectDataa()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(CreateBarCocktailWithCorrectDataa));
            var mapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var searchMapperMock = new Mock<IDtoMapper<Bar, SearchBarDto>>();
            var cocktailMapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var barGuid = Guid.NewGuid();
            var cocktailGuid = Guid.NewGuid();

            var entityDto = new BarDto
            {
                Id = barGuid,
                Name = "testBar",
                Info = "testInfo",
                Address = "testAddress",
                ImagePath = "testImagePath",
                Phone = "111-333-666"
            };

            var bar = new Bar
            {
                Id = barGuid,
                Name = "testBar",
                Info = "testInfo",
                Address = "testAddress",
                ImagePath = "testImagePath",
                Phone = "111-333-666"
            };

            var cocktail = new Cocktail
            {
                Id = cocktailGuid,
                Name = "testCocktail",
                Info = "testCocktailInfo",
            };

            var list = new List<string>() { "testCocktail" };

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Bars.AddAsync(bar);
                await arrangeContext.Cocktails.AddAsync(cocktail);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarService(assertContext, mapperMock.Object, searchMapperMock.Object, cocktailMapperMock.Object);
                var result = await sut.AddCocktailsAsync(entityDto, list);
                var barCocktail = await assertContext.BarCocktails.FirstAsync();
                Assert.AreEqual(bar.Id, barCocktail.BarId);
                Assert.AreEqual(cocktail.Id, barCocktail.CocktailId);
            }
        }

        [TestMethod]
        public async Task ThrowWhen_NobarFound()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ThrowWhen_NobarFound));
            var mapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var searchMapperMock = new Mock<IDtoMapper<Bar, SearchBarDto>>();
            var cocktailMapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var guid = Guid.NewGuid();

            var entityDto = new BarDto
            {
                Id = Guid.NewGuid(),
                Name = "testBar",
                Info = "testInfo",
                Address = "testAddress",
                ImagePath = "testImagePath",
                Phone = "111-333-666"
            };

            var bar = new Bar
            {
                Id = guid,
                Name = "testBar",
                Info = "testInfo",
                Address = "testAddress",
                ImagePath = "testImagePath",
                Phone = "111-333-666"
            };

            var cocktail = new Cocktail
            {
                Id = Guid.NewGuid(),
                Name = "testCocktail",
                Info = "testCocktailInfo",
            };

            var list = new List<string>() { "testCocktail" };

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Bars.AddAsync(bar);
                await arrangeContext.Cocktails.AddAsync(cocktail);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarService(assertContext, mapperMock.Object, searchMapperMock.Object, cocktailMapperMock.Object);
                await Assert.ThrowsExceptionAsync<BusinessLogicException>(() => sut.AddCocktailsAsync(entityDto, list));
            }
        }

        [TestMethod]
        public async Task ThrowWhen_ListIsEmpty()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ThrowWhen_ListIsEmpty));
            var mapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var searchMapperMock = new Mock<IDtoMapper<Bar, SearchBarDto>>();
            var cocktailMapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var guid = Guid.NewGuid();

            var entityDto = new BarDto
            {
                Id = Guid.NewGuid(),
                Name = "testBar",
                Info = "testInfo",
                Address = "testAddress",
                ImagePath = "testImagePath",
                Phone = "111-333-666"
            };

            var bar = new Bar
            {
                Id = guid,
                Name = "testBar",
                Info = "testInfo",
                Address = "testAddress",
                ImagePath = "testImagePath",
                Phone = "111-333-666"
            };

            var cocktail = new Cocktail
            {
                Id = Guid.NewGuid(),
                Name = "testCocktail",
                Info = "testCocktailInfo",
            };

            var list = new List<string>();

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Bars.AddAsync(bar);
                await arrangeContext.Cocktails.AddAsync(cocktail);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarService(assertContext, mapperMock.Object, searchMapperMock.Object, cocktailMapperMock.Object);
                await Assert.ThrowsExceptionAsync<BusinessLogicException>(() => sut.AddCocktailsAsync(entityDto, list));
            }
        }
    }
}
