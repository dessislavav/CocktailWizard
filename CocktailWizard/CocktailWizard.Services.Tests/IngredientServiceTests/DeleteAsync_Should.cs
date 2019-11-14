using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoMappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.IngredientServiceTests
{
    [TestClass]
    public class DeleteAsync_Should
    {
        [TestMethod]
        public async Task ThrowWhen_IngredientIsUsedInCocktails()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ThrowWhen_IngredientIsUsedInCocktails));
            var mapper = new IngredientDtoMapper();
            var ingTestGuid = Guid.NewGuid();
            var cocktailTestGuid = Guid.NewGuid();

            var ingredient = new Ingredient
            {
                Id = ingTestGuid,
                Name = "djodjan",
                IsDeleted = false,
            };

            var cocktail = new Cocktail
            {
                Id = cocktailTestGuid,
                Name = "test"
            };

            var cocktailIngr = new CocktailIngredient
            {
                CocktailId = cocktail.Id,
                IngredientId = ingredient.Id,
            };

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Ingredients.AddAsync(ingredient);
                await arrangeContext.Cocktails.AddAsync(cocktail);
                await arrangeContext.CocktailIngredients.AddAsync(cocktailIngr);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new IngredientService(assertContext, mapper);
                await Assert.ThrowsExceptionAsync<BusinessLogicException>(() => sut.DeleteAsync(ingredient.Id));
            }
        }

        [TestMethod]
        public async Task UpdatePropertiesWhen_ValuesAreValid()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(UpdatePropertiesWhen_ValuesAreValid));
            var mapper = new IngredientDtoMapper();
            var ingTestGuid = Guid.NewGuid();
            var cocktailTestGuid = Guid.NewGuid();

            var ingredient = new Ingredient
            {
                Id = ingTestGuid,
                Name = "djodjan",
                IsDeleted = false,
            };

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                await assertContext.Ingredients.AddAsync(ingredient);
                await assertContext.SaveChangesAsync();
                var sut = new IngredientService(assertContext, mapper);
                var result = await sut.DeleteAsync(ingTestGuid);
                Assert.AreEqual(true, ingredient.IsDeleted);
            }
        }

        [TestMethod]
        public async Task ReturnCorrectEntity_OnSuccess()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectEntity_OnSuccess));
            var mapper = new IngredientDtoMapper();
            var ingTestGuid = Guid.NewGuid();
            var cocktailTestGuid = Guid.NewGuid();

            var ingredient = new Ingredient
            {
                Id = ingTestGuid,
                Name = "djodjan",
                IsDeleted = false,
            };

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                await assertContext.Ingredients.AddAsync(ingredient);
                await assertContext.SaveChangesAsync();
                var sut = new IngredientService(assertContext, mapper);
                var result = await sut.DeleteAsync(ingTestGuid);
                Assert.IsInstanceOfType(result, typeof(IngredientDto));
            }
        }
    }
}
