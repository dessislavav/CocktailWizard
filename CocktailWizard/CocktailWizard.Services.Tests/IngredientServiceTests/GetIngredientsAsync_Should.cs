using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoMappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.IngredientServiceTests
{
    [TestClass]
    public class GetIngredientsAsync_Should
    {
        [TestMethod]
        public async Task ReturnInstanceOfCollectionTypeIngredientDto()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnInstanceOfCollectionTypeIngredientDto));
            var mapper = new IngredientDtoMapper();
            var testGuid = new Guid();
            var testGuid2 = new Guid();

            var entity = new Ingredient
            {
                Id = testGuid,
                Name = "djodjan",
                IsDeleted = false
            };
            var entity2 = new Ingredient
            {
                Id = testGuid2,
                Name = "testIngredient",
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
                var ingredient = await sut.GetIngredientsAsync();
                Assert.IsInstanceOfType(ingredient, typeof(ICollection<IngredientDto>));
            }
        }


        [TestMethod]
        public async Task ThrowWhen_DatabaseHasNoIngredients()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ThrowWhen_DatabaseHasNoIngredients));
            var mapper = new IngredientDtoMapper();

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new IngredientService(assertContext, mapper);
                var asd = sut.GetIngredientsAsync();
                await Assert.ThrowsExceptionAsync<BusinessLogicException> (() => sut.GetIngredientsAsync());
            }
        }

        [TestMethod]
        public async Task ReturnCorrectCountOfTypeIngredientDto()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectCountOfTypeIngredientDto));
            var mapper = new IngredientDtoMapper();
            var testGuid = new Guid();
            var testGuid2 = new Guid();
            var testGuid3 = new Guid();

            var entity = new Ingredient
            {
                Id = testGuid,
                Name = "djodjan",
                IsDeleted = false
            };
            var entity2 = new Ingredient
            {
                Id = testGuid2,
                Name = "testIngredient",
                IsDeleted = false
            };
            var entity3 = new Ingredient
            {
                Id = testGuid3,
                Name = "testIngredient2",
                IsDeleted = false
            };

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Ingredients.AddAsync(entity);
                await arrangeContext.Ingredients.AddAsync(entity2);
                await arrangeContext.Ingredients.AddAsync(entity3);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new IngredientService(assertContext, mapper);
                var ingredient = await sut.GetIngredientsAsync(2);
                Assert.AreEqual(2, ingredient.Count);
            }
        }
    }
}