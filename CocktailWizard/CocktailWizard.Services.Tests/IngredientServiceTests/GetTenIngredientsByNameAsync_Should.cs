using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
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

        [TestMethod]
        public async Task ReturnCorrectTypeOfCollectionWhen_ValidValueIsPassedAndPageIsSecond()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectTypeOfCollectionWhen_ValidValueIsPassedAndPageIsSecond));
            var mapperMock = new Mock<IDtoMapper<Ingredient, IngredientDto>>();

            var testGuid = Guid.NewGuid();
            var testGuid2 = Guid.NewGuid();
            var testGuid3 = Guid.NewGuid();
            var testGuid4 = Guid.NewGuid();
            var testGuid5 = Guid.NewGuid();
            var testGuid6 = Guid.NewGuid();
            var testGuid7 = Guid.NewGuid();
            var testGuid8 = Guid.NewGuid();
            var testGuid9 = Guid.NewGuid();
            var testGuid10 = Guid.NewGuid();
            var testGuid11 = Guid.NewGuid();
            var testGuid12 = Guid.NewGuid();

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
            var entity3 = new Ingredient
            {
                Id = testGuid3,
                Name = "djodjan3",
                IsDeleted = false
            };
            var entity4 = new Ingredient
            {
                Id = testGuid4,
                Name = "djodjan4",
                IsDeleted = false
            };
            var entity5 = new Ingredient
            {
                Id = testGuid5,
                Name = "djodjan5",
                IsDeleted = false
            };
            var entity6 = new Ingredient
            {
                Id = testGuid6,
                Name = "djodjan6",
                IsDeleted = false
            };
            var entity7 = new Ingredient
            {
                Id = testGuid7,
                Name = "djodjan7",
                IsDeleted = false
            };
            var entity8 = new Ingredient
            {
                Id = testGuid8,
                Name = "djodjan8",
                IsDeleted = false
            };
            var entity9 = new Ingredient
            {
                Id = testGuid9,
                Name = "djodjan9",
                IsDeleted = false
            };
            var entity10 = new Ingredient
            {
                Id = testGuid10,
                Name = "djodjan10",
                IsDeleted = false
            };
            var entity11 = new Ingredient
            {
                Id = testGuid12,
                Name = "djodjan12",
                IsDeleted = false
            };
            var entity12 = new Ingredient
            {
                Id = testGuid12,
                Name = "djodjan12",
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
                var result = await sut.GetTenIngredientsOrderedByNameAsync(2);
                Assert.IsInstanceOfType(result, typeof(ICollection<IngredientDto>));
                Assert.AreEqual(result.Count, 2);
            }
        }
    }
}