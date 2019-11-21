using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.Contracts;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.CocktailServiceTests
{
    [TestClass]
    public class DeleteAsync_Should
    {
        [TestMethod]

        public async Task CorrectlyDeleteCocktail()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(CorrectlyDeleteCocktail));

            var mapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var barMapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var cocktailDetailsMapperMock = new Mock<IDtoMapper<Cocktail, DetailsCocktailDto>>();
            var ingredientMapperMock = new Mock<IDtoMapper<Ingredient, IngredientDto>>();

            var ingredientServiceMock = new Mock<IIngredientService>();
            var cocktailIngredientServiceMock = new Mock<ICocktailIngredientService>();

            var testGuid = Guid.NewGuid();

            var cocktail = new Cocktail
            {
                Id = testGuid,
                Name = "TestOneName",
                ImagePath = "ImagePathOne"
            };

            using (var actContext = new CWContext(options))
            {
                //Act
                await actContext.Cocktails.AddAsync(cocktail);
                await actContext.SaveChangesAsync();
                var service = new CocktailService(actContext, mapperMock.Object, barMapperMock.Object, cocktailDetailsMapperMock.Object,
                    ingredientServiceMock.Object, cocktailIngredientServiceMock.Object);
                var result = await service.DeleteAsync(testGuid);
                await actContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Assert

                var result = await assertContext.Cocktails.FirstAsync();
                Assert.AreEqual(true, result.IsDeleted);
            }
        }

        [TestMethod]
        public async Task ThrowWhen_NoCocktailFound()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ThrowWhen_NoCocktailFound));

            var mapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var barMapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var cocktailDetailsMapperMock = new Mock<IDtoMapper<Cocktail, DetailsCocktailDto>>();
            var ingredientMapperMock = new Mock<IDtoMapper<Ingredient, IngredientDto>>();

            var ingredientServiceMock = new Mock<IIngredientService>();
            var cocktailIngredientServiceMock = new Mock<ICocktailIngredientService>();

            var testGuid = Guid.NewGuid();

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new CocktailService(assertContext, mapperMock.Object, barMapperMock.Object, cocktailDetailsMapperMock.Object,
                    ingredientServiceMock.Object, cocktailIngredientServiceMock.Object);

                await Assert.ThrowsExceptionAsync<BusinessLogicException>(() => sut.DeleteAsync(testGuid));
            }

        }

        [TestMethod]
        public async Task ReturnCorrectTypeOfInstance()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectTypeOfInstance));

            var mapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var barMapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var cocktailDetailsMapperMock = new Mock<IDtoMapper<Cocktail, DetailsCocktailDto>>();
            var ingredientMapperMock = new Mock<IDtoMapper<Ingredient, IngredientDto>>();

            var ingredientServiceMock = new Mock<IIngredientService>();
            var cocktailIngredientServiceMock = new Mock<ICocktailIngredientService>();

            var testGuid = Guid.NewGuid();

            var entity = new Cocktail
            {
                Id = testGuid,
                Name = "TestOneName",
                ImagePath = "ImagePathOne",
                Info = "TestInfo"
            };

            var dtoEntity = new CocktailDto
            {
                Id = testGuid,
                Name = "TestOneName",
                ImagePath = "ImagePathOne",
                Info = "TestInfo"

            };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<Cocktail>())).Returns(dtoEntity);

            using (var actContext = new CWContext(options))
            {
                //Act
                await actContext.Cocktails.AddAsync(entity);
                await actContext.SaveChangesAsync();
            }
            using (var assertContext = new CWContext(options))
            {
                //Assert
                var sut = new CocktailService(assertContext, mapperMock.Object, barMapperMock.Object, cocktailDetailsMapperMock.Object,
                    ingredientServiceMock.Object, cocktailIngredientServiceMock.Object);
                var result = await sut.DeleteAsync(testGuid);
                Assert.IsInstanceOfType(result, typeof(CocktailDto));
                Assert.AreEqual(dtoEntity.Name, result.Name);
                Assert.AreEqual(dtoEntity.Info, result.Info);
                Assert.AreEqual(dtoEntity.ImagePath, result.ImagePath);
                Assert.AreEqual(dtoEntity.Id, result.Id);
            }
        }

    }

}

