using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.DtoMappers;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.IngredientServiceTests
{
    [TestClass]
    public class GetIngredientAsync_Should
    {
        [TestMethod]
        public async Task ReturnInstanceOfTypeIngredientDto()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnInstanceOfTypeIngredientDto));
            var mapperMock = new Mock<IDtoMapper<Ingredient, IngredientDto>>();
            var testGuid = new Guid("b9653c65-2311-4d57-a95b-6522d7bc88f6");

            var entity = new Ingredient
            {
                Id = testGuid,
                Name = "djodjan6",
                IsDeleted = false
            };

            var ingredientDto = new IngredientDto
            {
                Id = testGuid,
                Name = "djodjan",
            };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<Ingredient>())).Returns(ingredientDto);

            using (var actContext = new CWContext(options))
            {
                //Act
                await actContext.Ingredients.AddAsync(entity);
                await actContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Assert
                var sut = new IngredientService(assertContext, mapperMock.Object);
                var ingredient = await sut.GetIngredientAsync(testGuid);
                Assert.IsInstanceOfType(ingredient, typeof(IngredientDto));
            }
        }

        [TestMethod]
        public async Task ReturnInstanceOfTypeIngredient()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnInstanceOfTypeIngredient));
            var mapperMock = new Mock<IDtoMapper<Ingredient, IngredientDto>>();
            var testGuid = new Guid("b9653c65-2311-4d57-a95b-6522d7bc88f4");

            var entity = new Ingredient
            {
                Id = testGuid,
                Name = "djodjan5",
                IsDeleted = false
            };

            using (var actContext = new CWContext(options))
            {
                //Act
                await actContext.Ingredients.AddAsync(entity);
                await actContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Assert
                var sut = new IngredientService(assertContext, mapperMock.Object);
                var ingredient = await sut.GetIngredientAsync("djodjan5");
                Assert.IsInstanceOfType(ingredient, typeof(Ingredient));
            }
        }

        [TestMethod]
        public async Task ReturnNullWhen_IngredientIdIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnNullWhen_IngredientIdIsNull));
            var mapperMock = new Mock<IDtoMapper<Ingredient, IngredientDto>>();
            var testGuid = new Guid("b9653c65-2311-4d57-a95b-6522d7bc88f3");

            var entity = new Ingredient
            {
                Id = testGuid,
                Name = "djodjan4",
                IsDeleted = false
            };

            using (var actContext = new CWContext(options))
            {
                //Act
                await actContext.Ingredients.AddAsync(entity);
                await actContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Assert
                var sut = new IngredientService(assertContext, mapperMock.Object);
                var ingredient = await sut.GetIngredientAsync(null);
                Assert.IsNull(ingredient);
            }
        }

        [TestMethod]
        public async Task ReturnNullWhen_IngredientIsDeleted()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnNullWhen_IngredientIsDeleted));
            var mapperMock = new Mock<IDtoMapper<Ingredient, IngredientDto>>();
            var testGuid = new Guid("b9653c65-2311-4d57-a95b-6522d7bc88f2");

            var entity = new Ingredient
            {
                Id = testGuid,
                Name = "djodjan3",
                IsDeleted = true
            };

            using (var actContext = new CWContext(options))
            {
                //Act
                await actContext.Ingredients.AddAsync(entity);
                await actContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Assert
                var sut = new IngredientService(assertContext, mapperMock.Object);
                var ingredient = await sut.GetIngredientAsync(testGuid);
                Assert.IsNull(ingredient);
            }
        }

        [TestMethod]
        public async Task ReturnNullWhen_IngredientIsDeletedByString()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnNullWhen_IngredientIsDeletedByString));
            var mapperMock = new Mock<IDtoMapper<Ingredient, IngredientDto>>();
            var testGuid = new Guid("b9653c65-2311-4d57-a95b-6522d7bc88f0");

            var entity = new Ingredient
            {
                Id = testGuid,
                Name = "djodjan2",
                IsDeleted = true
            };

            using (var actContext = new CWContext(options))
            {
                //Act
                await actContext.Ingredients.AddAsync(entity);
                await actContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Assert
                var sut = new IngredientService(assertContext, mapperMock.Object);
                var ingredient = await sut.GetIngredientAsync("djodjan2");
                Assert.IsNull(ingredient);
            }
        }
    }
}