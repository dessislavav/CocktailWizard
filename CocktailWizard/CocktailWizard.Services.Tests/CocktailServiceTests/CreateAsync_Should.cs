using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.Contracts;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.CocktailServiceTests
{
    [TestClass]

    public class CreateAsync_Should
    {
        [TestMethod]
        public async Task CorrectlyCreateCocktail()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(CorrectlyCreateCocktail));

            var mapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var barMapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var cocktailDetailsMapperMock = new Mock<IDtoMapper<Cocktail, DetailsCocktailDto>>();
            var ingredientMapperMock = new Mock<IDtoMapper<Ingredient, IngredientDto>>();

            var ingredientServiceMock = new Mock<IIngredientService>();
            var cocktailIngredientServiceMock = new Mock<ICocktailIngredientService>();

            string[] ingredients = new string[] { "TestIngredient"};

            var entityDto = new CocktailDto
            {
                Id = Guid.NewGuid(),
                Name = "TestName",
                Info = "TestInfo",
                ImagePath = "TestImagePath",
                CocktailIngredients = ingredients
            };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<Cocktail>())).Returns(entityDto);

            var ingredientId = Guid.NewGuid();

            var ingredient = new Ingredient
            {
                Id = ingredientId,
                Name = "TestIngredient"
            };

            var ingredientDto = new IngredientDto
            {
                Id = ingredientId,
                Name = "TestIngredient"
            };

            ingredientMapperMock.Setup(x => x.MapFrom(ingredient)).Returns(ingredientDto);

            var cocktailIngredient = new CocktailIngredient
            {
                CocktailId = entityDto.Id,
                IngredientId = ingredient.Id
            };


            ingredientServiceMock.Setup(x => x.GetIngredientAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(ingredient));
            cocktailIngredientServiceMock.Setup(x => x.CreateCocktailIngredientAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.FromResult(cocktailIngredient));


            using (var assertContext = new CWContext(options))
            {
                //Assert

                var sut = new CocktailService(assertContext, mapperMock.Object, barMapperMock.Object, cocktailDetailsMapperMock.Object,
                    ingredientServiceMock.Object, cocktailIngredientServiceMock.Object);
                var result = await sut.CreateAsync(entityDto);

                Assert.IsInstanceOfType(result, typeof(CocktailDto));
                Assert.AreEqual("TestName", result.Name);
                Assert.AreEqual("TestInfo", result.Info);
                Assert.AreEqual("TestImagePath", result.ImagePath);

                Assert.AreEqual(entityDto.Name, result.Name);
                Assert.AreEqual(entityDto.Info, result.Info);
                Assert.AreEqual(entityDto.ImagePath, result.ImagePath);

            }

        }


        [TestMethod]
        public async Task ThrowWhen_DtoPassedIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ThrowWhen_DtoPassedIsNull));

            var mapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var barMapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var cocktailDetailsMapperMock = new Mock<IDtoMapper<Cocktail, DetailsCocktailDto>>();
            var ingredientMapperMock = new Mock<IDtoMapper<Ingredient, IngredientDto>>();

            var ingredientServiceMock = new Mock<IIngredientService>();
            var cocktailIngredientServiceMock = new Mock<ICocktailIngredientService>();

            mapperMock.Setup(x => x.MapFrom(It.IsAny<Cocktail>())).Returns(It.IsAny<CocktailDto>);

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new CocktailService(assertContext, mapperMock.Object, barMapperMock.Object, cocktailDetailsMapperMock.Object,
                    ingredientServiceMock.Object, cocktailIngredientServiceMock.Object);
                await Assert.ThrowsExceptionAsync<BusinessLogicException>(() => sut.CreateAsync(null));
            }
        }

        [TestMethod]
        public async Task ThrowWhen_CocktailInredientsIsEmpty()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ThrowWhen_CocktailInredientsIsEmpty));

            var mapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var barMapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var cocktailDetailsMapperMock = new Mock<IDtoMapper<Cocktail, DetailsCocktailDto>>();
            var ingredientMapperMock = new Mock<IDtoMapper<Ingredient, IngredientDto>>();

            var ingredientServiceMock = new Mock<IIngredientService>();
            var cocktailIngredientServiceMock = new Mock<ICocktailIngredientService>();

            string[] ingredients = new string[] {};

            var entityDto = new CocktailDto
            {
                Id = Guid.NewGuid(),
                Name = "TestName",
                Info = "TestInfo",
                ImagePath = "TestImagePath",
                CocktailIngredients = ingredients
            };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<Cocktail>())).Returns(It.IsAny<CocktailDto>);


            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new CocktailService(assertContext, mapperMock.Object, barMapperMock.Object, cocktailDetailsMapperMock.Object,
                    ingredientServiceMock.Object, cocktailIngredientServiceMock.Object);

                await Assert.ThrowsExceptionAsync<BusinessLogicException>(() => sut.CreateAsync(entityDto));
            }
        }
    }
}

