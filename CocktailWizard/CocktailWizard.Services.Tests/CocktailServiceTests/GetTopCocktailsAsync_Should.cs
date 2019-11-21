using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.Contracts;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoMappers.Contracts;
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
    public class GetTopCocktailsAsync_Should
    {
        [TestMethod]
        public async Task ReturnCorrectInstanceOfCollection()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectInstanceOfCollection));

            var mapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var barMapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var cocktailDetailsMapperMock = new Mock<IDtoMapper<Cocktail, DetailsCocktailDto>>();
            var ingredientMapperMock = new Mock<IDtoMapper<Ingredient, IngredientDto>>();

            var ingredientServiceMock = new Mock<IIngredientService>();
            var cocktailIngredientServiceMock = new Mock<ICocktailIngredientService>();

            var testGuid = Guid.NewGuid();
            var testGuid2 = Guid.NewGuid();

            var cocktail1 = new Cocktail
            {
                Id = testGuid,
                Name = "TestOneName",
                ImagePath = "ImagePathOne"
            };

            var cocktail2 = new Cocktail
            {
                Id = testGuid2,
                Name = "TestTwoName",
                ImagePath = "ImagePathTwo"

            };

            var list = new List<CocktailDto>()
            {
                new CocktailDto{ Id = testGuid, Name = "TestOneName" },
                new CocktailDto { Id = testGuid2, Name = "TestTwoName"},
            };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<ICollection<Cocktail>>())).Returns(list);

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Cocktails.AddAsync(cocktail1);
                await arrangeContext.Cocktails.AddAsync(cocktail2);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new CocktailService(assertContext, mapperMock.Object, barMapperMock.Object, cocktailDetailsMapperMock.Object,
                    ingredientServiceMock.Object, cocktailIngredientServiceMock.Object);

                var result = await sut.GetTopCocktailsAsync(2);

                Assert.IsInstanceOfType(result, typeof(ICollection<CocktailDto>));
            }
        }

        [TestMethod]
        public async Task ReturnCorrectCountOfDtos()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectCountOfDtos));

            var mapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var barMapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var cocktailDetailsMapperMock = new Mock<IDtoMapper<Cocktail, DetailsCocktailDto>>();
            var ingredientMapperMock = new Mock<IDtoMapper<Ingredient, IngredientDto>>();

            var ingredientServiceMock = new Mock<IIngredientService>();
            var cocktailIngredientServiceMock = new Mock<ICocktailIngredientService>();

            var testGuid = Guid.NewGuid();
            var testGuid2 = Guid.NewGuid();

            var cocktail1 = new Cocktail
            {
                Id = testGuid,
                Name = "TestOneName",
                ImagePath = "ImagePathOne"
            };

            var cocktail2 = new Cocktail
            {
                Id = testGuid2,
                Name = "TestTwoName",
                ImagePath = "ImagePathTwo"

            };

            var list = new List<CocktailDto>()
            {
                new CocktailDto{ Id = testGuid, Name = "TestOneName" },
                new CocktailDto { Id = testGuid2, Name = "TestTwoName"},
            };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<ICollection<Cocktail>>())).Returns(list);

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Cocktails.AddAsync(cocktail1);
                await arrangeContext.Cocktails.AddAsync(cocktail2);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new CocktailService(assertContext, mapperMock.Object, barMapperMock.Object, cocktailDetailsMapperMock.Object,
                    ingredientServiceMock.Object, cocktailIngredientServiceMock.Object);

                var result = await sut.GetTopCocktailsAsync(2);

                Assert.AreEqual(2, result.Count());
            }
        }

        [TestMethod]
        public async Task ThrowWhen_NoCocktailsFound()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ThrowWhen_NoCocktailsFound));

            var mapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var barMapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var cocktailDetailsMapperMock = new Mock<IDtoMapper<Cocktail, DetailsCocktailDto>>();
            var ingredientMapperMock = new Mock<IDtoMapper<Ingredient, IngredientDto>>();

            var ingredientServiceMock = new Mock<IIngredientService>();
            var cocktailIngredientServiceMock = new Mock<ICocktailIngredientService>();

            mapperMock.Setup(x => x.MapFrom(It.IsAny<ICollection<Cocktail>>())).Returns(It.IsAny<ICollection<CocktailDto>>);

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new CocktailService(assertContext, mapperMock.Object, barMapperMock.Object, cocktailDetailsMapperMock.Object,
                    ingredientServiceMock.Object, cocktailIngredientServiceMock.Object);

                await Assert.ThrowsExceptionAsync<BusinessLogicException>(() => sut.GetTopCocktailsAsync(1));
            }
        }
    }
}
