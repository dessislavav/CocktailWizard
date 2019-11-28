using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
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
            var mapperMock = new Mock<IDtoMapper<Ingredient, IngredientDto>>();

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

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Ingredients.AddAsync(entity);
                await arrangeContext.Ingredients.AddAsync(entity2);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new IngredientService(assertContext, mapperMock.Object);
                var result = await sut.GetPageCountAsync(10);
                Assert.AreEqual(1, result);
            }
        }

        [TestMethod]
        public async Task ReturnCorrectType_ValidValueIsPassed()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectType_ValidValueIsPassed));
            var mapperMock = new Mock<IDtoMapper<Ingredient, IngredientDto>>();

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

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Ingredients.AddAsync(entity);
                await arrangeContext.Ingredients.AddAsync(entity2);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new IngredientService(assertContext, mapperMock.Object);
                var result = await sut.GetPageCountAsync(10);
                Assert.IsInstanceOfType(result, typeof(int));
            }
        }
    }
}
