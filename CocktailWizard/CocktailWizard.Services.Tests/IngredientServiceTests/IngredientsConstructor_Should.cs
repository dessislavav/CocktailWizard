using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.DtoMappers;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CocktailWizard.Services.Tests.IngredientServiceTests
{
    [TestClass]
    public class IngredientsConstructor_Should
    {
        [TestMethod]
        public void Constructor_CreatesInstance()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Constructor_CreatesInstance));
            var mapperMock = new Mock<IDtoMapper<Ingredient, IngredientDto>>();

            using (var assertContext = new CWContext(options))
            {
                //Act 
                var sut = new IngredientService(assertContext, mapperMock.Object);

                //Assert
                Assert.IsNotNull(sut);
            }
        }

        [TestMethod]
        public void Throw_WhenContextIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_WhenContextIsNull));
            var mapperMock = new Mock<IDtoMapper<Ingredient, IngredientDto>>();

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                Assert.ThrowsException<ArgumentNullException>(() => new IngredientService(null, mapperMock.Object));
            }
        }

        [TestMethod]
        public void Throw_WhenMapperIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_WhenMapperIsNull));

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                Assert.ThrowsException<ArgumentNullException>(() => new IngredientService(assertContext, null));
            }
        }
    }
}
