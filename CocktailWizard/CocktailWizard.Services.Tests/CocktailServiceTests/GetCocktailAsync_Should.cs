using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.Contracts;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.CocktailServiceTests
{
    [TestClass]
    public class GetCocktailAsync_Should
    {
        [TestMethod]
        public async Task ReturnCorrectDtoWhen_ParamsAreValid()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectDtoWhen_ParamsAreValid));

            var mapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var barMapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var cocktailDetailsMapperMock = new Mock<IDtoMapper<Cocktail, DetailsCocktailDto>>();
            var ingredientMapperMock = new Mock<IDtoMapper<Ingredient, IngredientDto>>();

            var ingredientServiceMock = new Mock<IIngredientService>();
            var cocktailIngredientServiceMock = new Mock<ICocktailIngredientService>();

            var testGuid = Guid.NewGuid();

            var cocktail = new Cocktail
            {
                Id = testGuid,
                Name = "TestOneName",
                ImagePath = "ImagePathOne"
            };

            var cocktailDto = new CocktailDto
            {
                Id = testGuid,
                Name = "TestOneName",
                ImagePath = "ImagePathOne"

            };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<Cocktail>())).Returns(cocktailDto);

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Cocktails.AddAsync(cocktail);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new CocktailService(assertContext, mapperMock.Object, barMapperMock.Object, cocktailDetailsMapperMock.Object,
                    ingredientServiceMock.Object, cocktailIngredientServiceMock.Object);

                var result = await sut.GetCocktailAsync(testGuid);
                Assert.IsInstanceOfType(result, typeof(CocktailDto));
                Assert.AreEqual("TestOneName", result.Name);
            }
        }

        [TestMethod]
        public async Task ThrowWhen_CocktailNotFound()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ThrowWhen_CocktailNotFound));

            var mapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var barMapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var cocktailDetailsMapperMock = new Mock<IDtoMapper<Cocktail, DetailsCocktailDto>>();
            var ingredientMapperMock = new Mock<IDtoMapper<Ingredient, IngredientDto>>();

            var ingredientServiceMock = new Mock<IIngredientService>();
            var cocktailIngredientServiceMock = new Mock<ICocktailIngredientService>();

            var testGuid = Guid.NewGuid();
            var testGuid2 = Guid.NewGuid();

            mapperMock.Setup(x => x.MapFrom(It.IsAny<Cocktail>())).Returns(It.IsAny<CocktailDto>);

            var cocktail = new Cocktail
            {
                Id = testGuid,
                Name = "TestOneName",
                ImagePath = "ImagePathOne"
            };

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Cocktails.AddAsync(cocktail);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new CocktailService(assertContext, mapperMock.Object, barMapperMock.Object, cocktailDetailsMapperMock.Object,
                    ingredientServiceMock.Object, cocktailIngredientServiceMock.Object);

                await Assert.ThrowsExceptionAsync<BusinessLogicException>(() => sut.GetCocktailAsync(testGuid2));
            }
        }
    }
}
