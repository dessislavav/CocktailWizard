using CocktailWizard.Data.AppContext;
using CocktailWizard.Services.DtoMappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CocktailWizard.Services.Tests.IngredientServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void Constructor_CreatesInstance()
        {
            //Arrange
            var options = TestUtilities.GetOptions(MethodBase.GetCurrentMethod().Name);
            var mapper = new IngredientDtoMapper();

            using (var assertContext = new CWContext(options))
            {
                //Act 
                var sut = new IngredientService(assertContext, mapper);

                //Assert
                Assert.IsNotNull(sut);
            }
        }

        [TestMethod]
        public void Throw_WhenContextIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_WhenContextIsNull));
            var mapper = new IngredientDtoMapper();

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                Assert.ThrowsException<ArgumentNullException>(() => new IngredientService(null, mapper));
            }
        }

        [TestMethod]
        public void Throw_WhenMapperIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_WhenContextIsNull));

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                Assert.ThrowsException<ArgumentNullException>(() => new IngredientService(assertContext, null));
            }
        }
    }
}
