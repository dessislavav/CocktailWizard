using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.DtoMappers;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.IngredientServiceTests
{
    [TestClass]
    public class CreateIngredientAsync_Should
    {
        [TestMethod]
        public async Task ReturnInstanceOfTypeIngredientDto()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnInstanceOfTypeIngredientDto));
            var mapperMock = new Mock<IDtoMapper<Ingredient, IngredientDto>>();

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new IngredientService(assertContext, mapperMock.Object);
                var result = await sut.CreateIngredientAsync("djodjan");
                Assert.IsInstanceOfType(result, typeof(Ingredient));
                Assert.AreEqual("djodjan", result.Name);
            }
        }

        [TestMethod]
        public async Task CorrectlyCreateIngredient()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(CorrectlyCreateIngredient));
            var mapperMock = new Mock<IDtoMapper<Ingredient, IngredientDto>>();

            var testGuid = Guid.NewGuid();

            var entityDto = new IngredientDto
            {
                Id = testGuid,
                Name = "djodjan",
            };

            var ingredientDto = new IngredientDto
            {
                Id = testGuid,
                Name = "djodjan",
            };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<Ingredient>())).Returns(ingredientDto);

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new IngredientService(assertContext, mapperMock.Object);
                var result = await sut.CreateIngredientAsync(entityDto);
                Assert.IsInstanceOfType(result, typeof(IngredientDto));
                Assert.AreEqual("djodjan", result.Name);
                Assert.AreEqual(entityDto.Name, result.Name);
            }
        }
    }
}
