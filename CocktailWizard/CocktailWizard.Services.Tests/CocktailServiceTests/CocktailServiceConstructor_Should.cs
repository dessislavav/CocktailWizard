using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.Contracts;
using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace CocktailWizard.Services.Tests.CocktailServiceTests
{
    [TestClass]
    public class CocktailServiceConstructor_Should
    {
        [TestMethod]
        public void CocktailServiceConstructor_CreatesInstance()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(CocktailServiceConstructor_CreatesInstance));

            var mapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var barMapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var cocktailDetailsMapperMock = new Mock<IDtoMapper<Cocktail, DetailsCocktailDto>>();

            var ingredientServiceMock = new Mock<IIngredientService>();
            var cocktailIngredientServiceMock = new Mock<ICocktailIngredientService>();

            using (var assertContext = new CWContext(options))
            {
                //Act
                var sut = new CocktailService(assertContext, mapperMock.Object, barMapperMock.Object,
                    cocktailDetailsMapperMock.Object, ingredientServiceMock.Object, cocktailIngredientServiceMock.Object);

                //Assert
                Assert.IsNotNull(sut);
            }
        }

        [TestMethod]
        public void Throw_WhenContextIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_WhenContextIsNull));

            var mapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var barMapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var cocktailDetailsMapperMock = new Mock<IDtoMapper<Cocktail, DetailsCocktailDto>>();

            var ingredientServiceMock = new Mock<IIngredientService>();
            var cocktailIngredientServiceMock = new Mock<ICocktailIngredientService>();

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                Assert.ThrowsException<ArgumentNullException>(() => new CocktailService(null, mapperMock.Object, barMapperMock.Object,
                    cocktailDetailsMapperMock.Object, ingredientServiceMock.Object, cocktailIngredientServiceMock.Object));
            }
        }

        [TestMethod]
        public void Throw_WhenCocktailMapperIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_WhenCocktailMapperIsNull));

            var mapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var barMapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var cocktailDetailsMapperMock = new Mock<IDtoMapper<Cocktail, DetailsCocktailDto>>();

            var ingredientServiceMock = new Mock<IIngredientService>();
            var cocktailIngredientServiceMock = new Mock<ICocktailIngredientService>();

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                Assert.ThrowsException<ArgumentNullException>(() => new CocktailService(assertContext, null, barMapperMock.Object,
                    cocktailDetailsMapperMock.Object, ingredientServiceMock.Object, cocktailIngredientServiceMock.Object));
            }
        }

        [TestMethod]
        public void Throw_WhenBarMapperIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_WhenBarMapperIsNull));

            var mapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var barMapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var cocktailDetailsMapperMock = new Mock<IDtoMapper<Cocktail, DetailsCocktailDto>>();

            var ingredientServiceMock = new Mock<IIngredientService>();
            var cocktailIngredientServiceMock = new Mock<ICocktailIngredientService>();

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                Assert.ThrowsException<ArgumentNullException>(() => new CocktailService(assertContext, mapperMock.Object, null,
                    cocktailDetailsMapperMock.Object, ingredientServiceMock.Object, cocktailIngredientServiceMock.Object));
            }
        }

        [TestMethod]
        public void Throw_WhenCocktailDetailsMapperIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_WhenCocktailDetailsMapperIsNull));

            var mapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var barMapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var cocktailDetailsMapperMock = new Mock<IDtoMapper<Cocktail, DetailsCocktailDto>>();

            var ingredientServiceMock = new Mock<IIngredientService>();
            var cocktailIngredientServiceMock = new Mock<ICocktailIngredientService>();

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                Assert.ThrowsException<ArgumentNullException>(() => new CocktailService(assertContext, mapperMock.Object, barMapperMock.Object,
                    null, ingredientServiceMock.Object, cocktailIngredientServiceMock.Object));
            }
        }

        [TestMethod]
        public void Throw_WhenIngredientServiceIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_WhenIngredientServiceIsNull));

            var mapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var barMapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var cocktailDetailsMapperMock = new Mock<IDtoMapper<Cocktail, DetailsCocktailDto>>();

            var ingredientServiceMock = new Mock<IIngredientService>();
            var cocktailIngredientServiceMock = new Mock<ICocktailIngredientService>();

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                Assert.ThrowsException<ArgumentNullException>(() => new CocktailService(assertContext, mapperMock.Object, barMapperMock.Object,
                    cocktailDetailsMapperMock.Object, null, cocktailIngredientServiceMock.Object));
            }
        }

        [TestMethod]
        public void Throw_WhenCocktailIngredientServiceIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_WhenCocktailIngredientServiceIsNull));

            var mapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var barMapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var cocktailDetailsMapperMock = new Mock<IDtoMapper<Cocktail, DetailsCocktailDto>>();

            var ingredientServiceMock = new Mock<IIngredientService>();
            var cocktailIngredientServiceMock = new Mock<ICocktailIngredientService>();

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                Assert.ThrowsException<ArgumentNullException>(() => new CocktailService(assertContext, mapperMock.Object, barMapperMock.Object,
                    cocktailDetailsMapperMock.Object, ingredientServiceMock.Object, null));
            }
        }
    }
}
