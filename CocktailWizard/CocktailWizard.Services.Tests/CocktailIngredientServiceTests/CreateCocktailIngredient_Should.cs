using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.CocktailIngredientServiceTests
{
    [TestClass]
    public class CreateCocktailIngredientAsync_Should
    {
        [TestMethod]
        public async Task ReturnInstanceOfTypeCocktailIngredient()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnInstanceOfTypeCocktailIngredient));
            var id = Guid.NewGuid();
            var id2 = Guid.NewGuid();

            var cocktail = new Cocktail
            {
                Id = id,
                Name = "testCocktail"
            };

            var ingredient = new Ingredient
            {
                Id = id2,
                Name = "testIngredient"
            };

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new CocktailIngredientService(assertContext);
                var result = await sut.CreateCocktailIngredientAsync(cocktail.Id, ingredient.Id);
                Assert.IsInstanceOfType(result, typeof(CocktailIngredient));
                Assert.AreEqual(cocktail.Id, result.CocktailId);
                Assert.AreEqual(ingredient.Id, result.IngredientId);
            }
        }
    }
}