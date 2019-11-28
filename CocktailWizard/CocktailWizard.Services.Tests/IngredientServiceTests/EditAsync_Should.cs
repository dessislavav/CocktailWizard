using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.IngredientServiceTests
{
    [TestClass]
    public class EditAsync_Should
    {
        [TestMethod]
        public async Task ThrowWhen_NewNameIsEmptyString()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ThrowWhen_NewNameIsEmptyString));
            var mapperMock = new Mock<IDtoMapper<Ingredient, IngredientDto>>();

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new IngredientService(assertContext, mapperMock.Object);
                await Assert.ThrowsExceptionAsync<BusinessLogicException>(() => sut.EditAsync(Guid.NewGuid(), String.Empty));
            }
        }

        [TestMethod]
        public async Task SetCorrectParam_WhenValueIsValid()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(SetCorrectParam_WhenValueIsValid));
            var mapperMock = new Mock<IDtoMapper<Ingredient, IngredientDto>>();
            var testGuid = Guid.NewGuid();

            var entity = new Ingredient
            {
                Id = testGuid,
                Name = "djodjan",
                IsDeleted = false
            };

            var ingredientDto = new IngredientDto
            {
                Id = testGuid,
                Name = "djodjan",
            };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<Ingredient>())).Returns(ingredientDto);

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Ingredients.AddAsync(entity);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new IngredientService(assertContext, mapperMock.Object);
                var result = await sut.EditAsync(testGuid, "newDjodjan");
                var ingredient = await assertContext.Ingredients.FirstAsync();
                Assert.AreEqual("newDjodjan", ingredient.Name);
            }
        }

        [TestMethod]
        public async Task SetNewParamsToCorrectEntity_WhenValueIsValid()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(SetNewParamsToCorrectEntity_WhenValueIsValid));
            var mapperMock = new Mock<IDtoMapper<Ingredient, IngredientDto>>();
            var testGuid = Guid.NewGuid();

            var entity = new Ingredient
            {
                Id = testGuid,
                Name = "djodjan",
                IsDeleted = false
            };

            var ingredientDto = new IngredientDto
            {
                Id = testGuid,
                Name = "djodjan",
            };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<Ingredient>())).Returns(ingredientDto);

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Ingredients.AddAsync(entity);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new IngredientService(assertContext, mapperMock.Object);
                var result = await sut.EditAsync(testGuid, "newDjodjan");
                Assert.AreEqual(entity.Id, result.Id);
            }
        }
    }
}
