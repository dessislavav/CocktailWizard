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
    public class GetPageCountAsync_Should
    {
        [TestMethod]
        public async Task ReturnCorrectCountWhen_ValidValueIsPassed()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectCountWhen_ValidValueIsPassed));
            var mapper = new IngredientDtoMapper();

            var entity = new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "djodjan1",
                IsDeleted = true
            };
            var entity2 = new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "djodjan2",
                IsDeleted = true
            };

            using (var actContext = new CWContext(options))
            {
                //Act
                await actContext.Ingredients.AddAsync(entity);
                await actContext.Ingredients.AddAsync(entity2);
                await actContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Assert
                var sut = new IngredientService(assertContext, mapper);
                var result = await sut.GetPageCountAsync(10);
                Assert.AreEqual(1, result);
            }
        }
    }
}
