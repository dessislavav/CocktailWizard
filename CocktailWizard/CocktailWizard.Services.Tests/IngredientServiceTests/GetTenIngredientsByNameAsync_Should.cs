using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
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
    public class GetTenIngredientsByNameAsync_Should
    {
        [TestMethod]
        public async Task ReturnCorrectTypeOfCollectionWhen_ValidValueIsPassed()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectTypeOfCollectionWhen_ValidValueIsPassed));
            var mapper = new IngredientDtoMapper();

            var entity = new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "djodjan1",
                IsDeleted = false
            };
            var entity2 = new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "djodjan2",
                IsDeleted = false
            };

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Ingredients.AddAsync(entity);
                await arrangeContext.Ingredients.AddAsync(entity2);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new IngredientService(assertContext, mapper);
                var result = await sut.GetTenIngredientsOrderedByNameAsync(1);
                Assert.IsInstanceOfType(result, typeof(ICollection<IngredientDto>));
                Assert.AreEqual(result.Count, 2);
            }
        }
    }
}