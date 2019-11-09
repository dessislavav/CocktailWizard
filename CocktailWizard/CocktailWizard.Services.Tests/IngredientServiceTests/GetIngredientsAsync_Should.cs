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
            var options = TestUtilities.GetOptions(MethodBase.GetCurrentMethod().Name);
            var mapper = new IngredientDtoMapper();
            var testGuid = new Guid("b9653c65-2311-4d57-a95b-6522d7bc88f1");
            var testGuid2 = new Guid("b9653c65-2311-4d57-a95b-6522d7bc88f6");

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
                var ingredient = await sut.GetIngredientsAsync();
                Assert.IsInstanceOfType(ingredient, typeof(ICollection<IngredientDto>));
            }
        }


        [TestMethod]
        public async Task ThrowWhen_DatabaseHasNoIngredients()
        {
            //Arrange
            var options = TestUtilities.GetOptions(MethodBase.GetCurrentMethod().Name);
            var mapper = new IngredientDtoMapper();

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new IngredientService(assertContext, mapper);
                var asd = sut.GetIngredientsAsync();
                await Assert.ThrowsExceptionAsync<BusinessLogicException> (() => sut.GetIngredientsAsync());
            }
        }
    }
}
