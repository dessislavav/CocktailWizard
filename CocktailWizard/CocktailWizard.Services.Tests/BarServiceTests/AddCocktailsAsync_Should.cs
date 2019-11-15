using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoMappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var searchMapper = new SearchBarDtoMapper();
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
                var sut = new BarService(assertContext, mapper, searchMapper);
                var result = await sut.AddCocktailsAsync(entityDto, list);
                //var barCocktail = await assertContext.BarCocktails.FirstAsync();
                Assert.AreEqual(1, await assertContext.BarCocktails.CountAsync());
                //Assert.IsInstanceOfType(result, typeof(BarDto));
            }
        }

        [TestMethod]
        public async Task ReturnCorectTypeOfInstance()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorectTypeOfInstance));
            var mapper = new BarDtoMapper();
            var searchMapper = new SearchBarDtoMapper();
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
                var sut = new BarService(assertContext, mapper, searchMapper);
                var result = await sut.AddCocktailsAsync(entityDto, list);
                Assert.IsInstanceOfType(result, typeof(BarDto));
            }
        }

        [TestMethod]
        public async Task CreateBarCocktailWithCorrectData()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(CreateBarCocktailWithCorrectData));
            var mapper = new BarDtoMapper();
            var searchMapper = new SearchBarDtoMapper();
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
                var sut = new BarService(assertContext, mapper, searchMapper);
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
            var mapper = new BarDtoMapper();
            var searchMapper = new SearchBarDtoMapper();
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
                var sut = new BarService(assertContext, mapper, searchMapper);
                await Assert.ThrowsExceptionAsync<BusinessLogicException>(() => sut.AddCocktailsAsync(entityDto, list));
            }
        }

        [TestMethod]
        public async Task ThrowWhen_ListIsEmpty()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ThrowWhen_ListIsEmpty));
            var mapper = new BarDtoMapper();
            var searchMapper = new SearchBarDtoMapper();
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
                var sut = new BarService(assertContext, mapper, searchMapper);
                await Assert.ThrowsExceptionAsync<BusinessLogicException>(() => sut.AddCocktailsAsync(entityDto, list));
            }
        }
    }
}
