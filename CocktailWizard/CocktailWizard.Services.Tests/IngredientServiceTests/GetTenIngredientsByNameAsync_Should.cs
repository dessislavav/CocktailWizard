using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.DtoMappers;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
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
            var mapperMock = new Mock<IDtoMapper<Ingredient, IngredientDto>>();

            var testGuid = Guid.NewGuid();
            var testGuid2 = Guid.NewGuid();

            var entity = new Ingredient
            {
                Id = testGuid,
                Name = "djodjan1",
                IsDeleted = false
            };
            var entity2 = new Ingredient
            {
                Id = testGuid2,
                Name = "djodjan2",
                IsDeleted = false
            };

            var list = new List<IngredientDto>() { new IngredientDto { Id = testGuid, Name = "djodjan" }, new IngredientDto { Id = testGuid2, Name = "testIngredient" } };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<ICollection<Ingredient>>())).Returns(list);

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
                var result = await sut.GetTenIngredientsOrderedByNameAsync(1);
                Assert.IsInstanceOfType(result, typeof(ICollection<IngredientDto>));
                Assert.AreEqual(result.Count, 2);
            }
        }
    }
}