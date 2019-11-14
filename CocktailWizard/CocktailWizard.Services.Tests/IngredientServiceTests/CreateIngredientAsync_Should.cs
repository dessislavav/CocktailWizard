using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.DtoMappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.IngredientServiceTests
{
    [TestClass]
    public class CreateIngredientAsync_Should
    {
        [TestMethod]
        public async Task ReturnInstanceOfTypeIngredientDto()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnInstanceOfTypeIngredientDto));
            var mapper = new IngredientDtoMapper();

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new IngredientService(assertContext, mapper);
                var result = await sut.CreateIngredientAsync("djodjan");
                Assert.IsInstanceOfType(result, typeof(Ingredient));
                Assert.AreEqual("djodjan", result.Name);
            }
        }
    }
}
