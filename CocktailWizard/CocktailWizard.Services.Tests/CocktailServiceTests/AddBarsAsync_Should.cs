using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.Contracts;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoMappers;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.CocktailServiceTests
{
    [TestClass]
    public class AddCocktailsAsync_Should
    {
        [TestMethod]
        public async Task CorrectlyAddBarsToCocktail()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(CorrectlyAddBarsToCocktail));
            var mapper = new CocktailDtoMapper();

            var mapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var barMapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var cocktailDetailsMapperMock = new Mock<IDtoMapper<Cocktail, DetailsCocktailDto>>();

            var ingredientServiceMock = new Mock<IIngredientService>();
            var cocktailIngredientServiceMock = new Mock<ICocktailIngredientService>();

            var guid = Guid.NewGuid();

            var entityDto = new CocktailDto
            {
                Id = guid,
                Name = "testCocktail",
                Info = "testInfo",
            };

            var bar = new Bar
            {
                Id = Guid.NewGuid(),
                Name = "testBar",
                Info = "testInfo",
                Address = "testAddress",
                ImagePath = "testImagePath",
                Phone = "111-333-666"
            };

            var cocktail = new Cocktail
            {
                Id = guid,
                Name = "testCocktail",
                Info = "testCocktailInfo",
            };

            var list = new List<string>() { "testBar" };

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
                var sut = new CocktailService(assertContext, mapperMock.Object, barMapperMock.Object,
                    cocktailDetailsMapperMock.Object, ingredientServiceMock.Object, cocktailIngredientServiceMock.Object);
                var result = await sut.AddBarsAsync(entityDto, list);
                Assert.AreEqual(1, await assertContext.BarCocktails.CountAsync());
            }
        }

        [TestMethod]
        public async Task ReturnCorectTypeOfInstance()
        { 

            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorectTypeOfInstance));
            var mapper = new CocktailDtoMapper();

            var mapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var barMapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var cocktailDetailsMapperMock = new Mock<IDtoMapper<Cocktail, DetailsCocktailDto>>();

            var ingredientServiceMock = new Mock<IIngredientService>();
            var cocktailIngredientServiceMock = new Mock<ICocktailIngredientService>();

            var guid = Guid.NewGuid();

            var entityDto = new CocktailDto
            {
                Id = guid,
                Name = "testCocktail",
                Info = "testInfo",
            };

            var bar = new Bar
            {
                Id = Guid.NewGuid(),
                Name = "testBar",
                Info = "testInfo",
                Address = "testAddress",
                ImagePath = "testImagePath",
                Phone = "111-333-666"
            };

            var cocktail = new Cocktail
            {
                Id = guid,
                Name = "testCocktail",
                Info = "testCocktailInfo",
            };

            var list = new List<string>() { "testBar" };

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
                var sut = new CocktailService(assertContext, mapperMock.Object, barMapperMock.Object,
                    cocktailDetailsMapperMock.Object, ingredientServiceMock.Object, cocktailIngredientServiceMock.Object);
                var result = await sut.AddBarsAsync(entityDto, list);
                Assert.AreEqual(1, await assertContext.BarCocktails.CountAsync());
            }
        }

        [TestMethod]
        public async Task CreateBarCocktailWithCorrectData()
        {

            //Arrange
            var options = TestUtilities.GetOptions(nameof(CreateBarCocktailWithCorrectData));
            var mapper = new CocktailDtoMapper();

            var mapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var barMapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var cocktailDetailsMapperMock = new Mock<IDtoMapper<Cocktail, DetailsCocktailDto>>();

            var ingredientServiceMock = new Mock<IIngredientService>();
            var cocktailIngredientServiceMock = new Mock<ICocktailIngredientService>();

            var guid = Guid.NewGuid();

            var entityDto = new CocktailDto
            {
                Id = guid,
                Name = "testCocktail",
                Info = "testInfo",
            };

            var bar = new Bar
            {
                Id = Guid.NewGuid(),
                Name = "testBar",
                Info = "testInfo",
                Address = "testAddress",
                ImagePath = "testImagePath",
                Phone = "111-333-666"
            };

            var cocktail = new Cocktail
            {
                Id = guid,
                Name = "testCocktail",
                Info = "testCocktailInfo",
            };

            var list = new List<string>() { "testBar" };

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
                var sut = new CocktailService(assertContext, mapperMock.Object, barMapperMock.Object,
                    cocktailDetailsMapperMock.Object, ingredientServiceMock.Object, cocktailIngredientServiceMock.Object);
                var result = await sut.AddBarsAsync(entityDto, list);
                var barCocktail = assertContext.BarCocktails.First();
                Assert.AreEqual(bar.Id, barCocktail.BarId);
                Assert.AreEqual(cocktail.Id, barCocktail.CocktailId);
            }
        }

        //[TestMethod]
        //public async Task ThrowWhen_NobarFound()
        //{
        //    //Arrange
        //    var options = TestUtilities.GetOptions(nameof(ThrowWhen_NobarFound));
        //    var mapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
        //    var searchMapperMock = new Mock<IDtoMapper<Bar, SearchBarDto>>();
        //    var cocktailMapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
        //    var guid = Guid.NewGuid();

        //    var entityDto = new BarDto
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "testBar",
        //        Info = "testInfo",
        //        Address = "testAddress",
        //        ImagePath = "testImagePath",
        //        Phone = "111-333-666"
        //    };

        //    var bar = new Bar
        //    {
        //        Id = guid,
        //        Name = "testBar",
        //        Info = "testInfo",
        //        Address = "testAddress",
        //        ImagePath = "testImagePath",
        //        Phone = "111-333-666"
        //    };

        //    var cocktail = new Cocktail
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "testCocktail",
        //        Info = "testCocktailInfo",
        //    };

        //    var list = new List<string>() { "testCocktail" };

        //    using (var arrangeContext = new CWContext(options))
        //    {
        //        await arrangeContext.Bars.AddAsync(bar);
        //        await arrangeContext.Cocktails.AddAsync(cocktail);
        //        await arrangeContext.SaveChangesAsync();
        //    }

        //    using (var assertContext = new CWContext(options))
        //    {
        //        //Act & Assert
        //        var sut = new BarService(assertContext, mapperMock.Object, searchMapperMock.Object, cocktailMapperMock.Object);
        //        await Assert.ThrowsExceptionAsync<BusinessLogicException>(() => sut.AddCocktailsAsync(entityDto, list));
        //    }
        //}

        //[TestMethod]
        //public async Task ThrowWhen_ListIsEmpty()
        //{
        //    //Arrange
        //    var options = TestUtilities.GetOptions(nameof(ThrowWhen_ListIsEmpty));
        //    var mapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
        //    var searchMapperMock = new Mock<IDtoMapper<Bar, SearchBarDto>>();
        //    var cocktailMapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
        //    var guid = Guid.NewGuid();

        //    var entityDto = new BarDto
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "testBar",
        //        Info = "testInfo",
        //        Address = "testAddress",
        //        ImagePath = "testImagePath",
        //        Phone = "111-333-666"
        //    };

        //    var bar = new Bar
        //    {
        //        Id = guid,
        //        Name = "testBar",
        //        Info = "testInfo",
        //        Address = "testAddress",
        //        ImagePath = "testImagePath",
        //        Phone = "111-333-666"
        //    };

        //    var cocktail = new Cocktail
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "testCocktail",
        //        Info = "testCocktailInfo",
        //    };

        //    var list = new List<string>();

        //    using (var arrangeContext = new CWContext(options))
        //    {
        //        await arrangeContext.Bars.AddAsync(bar);
        //        await arrangeContext.Cocktails.AddAsync(cocktail);
        //        await arrangeContext.SaveChangesAsync();
        //    }

        //    using (var assertContext = new CWContext(options))
        //    {
        //        //Act & Assert
        //        var sut = new BarService(assertContext, mapperMock.Object, searchMapperMock.Object, cocktailMapperMock.Object);
        //        await Assert.ThrowsExceptionAsync<BusinessLogicException>(() => sut.AddCocktailsAsync(entityDto, list));
        //    }
        //}
    }
}
