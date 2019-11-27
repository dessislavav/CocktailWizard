using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.Contracts;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.CocktailServiceTests
{
    [TestClass]
    public class SearchAsync_Should
    {
        [TestMethod]
        public async Task ReturnCorrectEntitiesWhenSearchedBy_GeneralSearch()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectEntitiesWhenSearchedBy_GeneralSearch));

            var mapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var barMapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var cocktailDetailsMapperMock = new Mock<IDtoMapper<Cocktail, DetailsCocktailDto>>();
            var ingredientMapperMock = new Mock<IDtoMapper<Ingredient, IngredientDto>>();

            var ingredientServiceMock = new Mock<IIngredientService>();
            var cocktailIngredientServiceMock = new Mock<ICocktailIngredientService>();

            string[] ingredients = new string[] { "TestIngredient", "TestIngredient2" };

            var cocktailId = Guid.NewGuid();
            var cocktail2Id = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var user2Id = Guid.NewGuid();

          
            var ingredientId = Guid.NewGuid();
            var ingredientId2 = Guid.NewGuid();

            var ingredient = new Ingredient
            {
                Id = ingredientId,
                Name = "TestIngredient"
            };

            var ingredientDto = new IngredientDto
            {
                Id = ingredientId,
                Name = "TestIngredient"
            };

            var ingredient2 = new Ingredient
            {
                Id = ingredientId2,
                Name = "TestIngredient2"
            };

            var ingredientDto2 = new IngredientDto
            {
                Id = ingredientId2,
                Name = "TestIngredient2"
            };

            ingredientMapperMock.Setup(x => x.MapFrom(ingredient)).Returns(ingredientDto);

            var cocktailIngredients = new List<CocktailIngredient> { new CocktailIngredient
            {
                CocktailId = cocktailId,
                IngredientId = ingredient.Id

            }, new CocktailIngredient
            {
                CocktailId = cocktail2Id,
                IngredientId = ingredient2.Id
            }};

            var cocktails = new List<Cocktail> { new Cocktail
            {
                Id = cocktailId,
                Name = "testCocktail",
                Info = "testInfo",
                ImagePath = "testImagePath",
                CocktailIngredients = cocktailIngredients

            }, new Cocktail
            {
                Id = cocktail2Id,
                Name = "testCocktail2",
                Info = "testInfo",
                ImagePath = "testImagePath2",
                CocktailIngredients = cocktailIngredients
            }};

            var searchCocktails = new List<CocktailDto> { new CocktailDto
            {
                Id = cocktailId,
                Name = "testCocktail",
                Info = "testInfo",
                ImagePath = "testImagePath",
                CocktailIngredients = ingredients

            }, new CocktailDto
            {
                Id = cocktail2Id,
                Name = "testCocktail2",
                Info = "testInfo",
                ImagePath = "testImagePath2",
                CocktailIngredients = ingredients

            }};

            var user = new User { Id = userId };
            var user2 = new User { Id = user2Id };

            var ratings = new List<CocktailRating> { new CocktailRating { CocktailId = cocktail2Id, UserId = userId, Value = 4 }, new CocktailRating { CocktailId = cocktail2Id, UserId = user2Id, Value = 5 } };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<ICollection<Cocktail>>())).Returns(searchCocktails);

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                await assertContext.Cocktails.AddAsync(cocktails[0]);
                await assertContext.Cocktails.AddAsync(cocktails[1]);
                await assertContext.Users.AddAsync(user);
                await assertContext.Users.AddAsync(user2);
                await assertContext.CocktailRatings.AddAsync(ratings[0]);
                await assertContext.CocktailRatings.AddAsync(ratings[1]);
                await assertContext.SaveChangesAsync();
                var service = new CocktailService(assertContext, mapperMock.Object, barMapperMock.Object, cocktailDetailsMapperMock.Object,
                    ingredientServiceMock.Object, cocktailIngredientServiceMock.Object);
                var result = await service.SearchAsync("testCocktail2", false, false, 1);

                Assert.AreEqual(1, result.Count);
            }
        }

        [TestMethod]
        public async Task ReturnCorrectEntitiesWhenSearchedBy_CertainCriteria()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectEntitiesWhenSearchedBy_CertainCriteria));

            var mapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var barMapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var cocktailDetailsMapperMock = new Mock<IDtoMapper<Cocktail, DetailsCocktailDto>>();
            var ingredientMapperMock = new Mock<IDtoMapper<Ingredient, IngredientDto>>();

            var ingredientServiceMock = new Mock<IIngredientService>();
            var cocktailIngredientServiceMock = new Mock<ICocktailIngredientService>();

            string[] ingredients = new string[] { "TestIngredient", "TestIngredient2" };

            var cocktailId = Guid.NewGuid();
            var cocktail2Id = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var user2Id = Guid.NewGuid();


            var ingredientId = Guid.NewGuid();
            var ingredientId2 = Guid.NewGuid();

            var ingredient = new Ingredient
            {
                Id = ingredientId,
                Name = "TestIngredient"
            };

            var ingredientDto = new IngredientDto
            {
                Id = ingredientId,
                Name = "TestIngredient"
            };

            var ingredient2 = new Ingredient
            {
                Id = ingredientId2,
                Name = "TestIngredient2"
            };

            var ingredientDto2 = new IngredientDto
            {
                Id = ingredientId2,
                Name = "TestIngredient2"
            };

            ingredientMapperMock.Setup(x => x.MapFrom(ingredient)).Returns(ingredientDto);

            var cocktailIngredients = new List<CocktailIngredient> { new CocktailIngredient
            {
                CocktailId = cocktailId,
                IngredientId = ingredient.Id

            }, new CocktailIngredient
            {
                CocktailId = cocktail2Id,
                IngredientId = ingredient2.Id
            }};

            var cocktails = new List<Cocktail> { new Cocktail
            {
                Id = cocktailId,
                Name = "testCocktail",
                Info = "testInfo",
                ImagePath = "testImagePath",
                CocktailIngredients = cocktailIngredients

            }, new Cocktail
            {
                Id = cocktail2Id,
                Name = "testCocktail2",
                Info = "testInfo",
                ImagePath = "testImagePath2",
                CocktailIngredients = cocktailIngredients
            }};

            var searchCocktails = new List<CocktailDto> { new CocktailDto
            {
                Id = cocktailId,
                Name = "testCocktail",
                Info = "testInfo",
                ImagePath = "testImagePath",
                CocktailIngredients = ingredients

            }, new CocktailDto
            {
                Id = cocktail2Id,
                Name = "testCocktail2",
                Info = "testInfo",
                ImagePath = "testImagePath2",
                CocktailIngredients = ingredients

            }};

            var user = new User { Id = userId };
            var user2 = new User { Id = user2Id };

            var ratings = new List<CocktailRating> { new CocktailRating { CocktailId = cocktail2Id, UserId = userId, Value = 4 }, new CocktailRating { CocktailId = cocktail2Id, UserId = user2Id, Value = 5 } };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<ICollection<Cocktail>>())).Returns(searchCocktails);

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                await assertContext.Cocktails.AddAsync(cocktails[0]);
                await assertContext.Cocktails.AddAsync(cocktails[1]);
                await assertContext.Users.AddAsync(user);
                await assertContext.Users.AddAsync(user2);
                await assertContext.CocktailRatings.AddAsync(ratings[0]);
                await assertContext.CocktailRatings.AddAsync(ratings[1]);
                await assertContext.SaveChangesAsync();
                var service = new CocktailService(assertContext, mapperMock.Object, barMapperMock.Object, cocktailDetailsMapperMock.Object,
                    ingredientServiceMock.Object, cocktailIngredientServiceMock.Object);
                var result = await service.SearchAsync("testCocktail2", true, false, 1);

                Assert.AreEqual(1, result.Count);
            }
        }
    }
}
