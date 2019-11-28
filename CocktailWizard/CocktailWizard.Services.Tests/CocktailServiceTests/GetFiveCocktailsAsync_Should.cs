using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.Contracts;
using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.CocktailServiceTests
{
    [TestClass]
    public class GetTenCocktailsOrderedByNameAsync_Should
    {
        [TestMethod]
        public async Task ReturnCorrectInstanceOfCollection()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectInstanceOfCollection));

            var mapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var barMapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var cocktailDetailsMapperMock = new Mock<IDtoMapper<Cocktail, DetailsCocktailDto>>();
            var ingredientMapperMock = new Mock<IDtoMapper<Ingredient, IngredientDto>>();

            var ingredientServiceMock = new Mock<IIngredientService>();
            var cocktailIngredientServiceMock = new Mock<ICocktailIngredientService>();


            var testGuid = Guid.NewGuid();
            var testGuid2 = Guid.NewGuid();
            var testGuid3 = Guid.NewGuid();

            var cocktail1 = new Cocktail
            {
                Id = testGuid,
                Name = "TestOneName",
                ImagePath = "ImagePathOne"
            };

            var cocktail2 = new Cocktail
            {
                Id = testGuid2,
                Name = "TestTwoName",
                ImagePath = "ImagePathTwo"

            };

            var cocktail3 = new Cocktail
            {
                Id = testGuid3,
                Name = "TestThreeName",
                ImagePath = "ImagePathThree"

            };

            var list = new List<CocktailDto>()
            {
                new CocktailDto{ Id = testGuid, Name = "TestOneName" },
                new CocktailDto { Id = testGuid2, Name = "TestTwoName"},
                new CocktailDto { Id = testGuid3, Name = "TestThreeName"}
            };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<ICollection<Cocktail>>())).Returns(list);

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Cocktails.AddAsync(cocktail1);
                await arrangeContext.Cocktails.AddAsync(cocktail2);
                await arrangeContext.Cocktails.AddAsync(cocktail3);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new CocktailService(assertContext, mapperMock.Object, barMapperMock.Object, cocktailDetailsMapperMock.Object,
                    ingredientServiceMock.Object, cocktailIngredientServiceMock.Object);
                var result = await sut.GetFiveCocktailsAsync(1, null);
                Assert.IsInstanceOfType(result, typeof(ICollection<CocktailDto>));
                Assert.AreEqual(3, result.Count());
            }
        }

        [TestMethod]
        public async Task ReturnCorrectInstanceOfCollection_FromSecondPage()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectInstanceOfCollection_FromSecondPage));

            var mapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var barMapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var cocktailDetailsMapperMock = new Mock<IDtoMapper<Cocktail, DetailsCocktailDto>>();
            var ingredientMapperMock = new Mock<IDtoMapper<Ingredient, IngredientDto>>();

            var ingredientServiceMock = new Mock<IIngredientService>();
            var cocktailIngredientServiceMock = new Mock<ICocktailIngredientService>();

            var testGuid = Guid.NewGuid();
            var testGuid2 = Guid.NewGuid();
            var testGuid3 = Guid.NewGuid();
            var testGuid4 = Guid.NewGuid();
            var testGuid5 = Guid.NewGuid();
            var testGuid6 = Guid.NewGuid();

            var cocktail1 = new Cocktail
            {
                Id = testGuid,
                Name = "TestOneName",
                ImagePath = "ImagePathOne"
            };

            var cocktail2 = new Cocktail
            {
                Id = testGuid2,
                Name = "TestTwoName",
                ImagePath = "ImagePathTwo"
            };

            var cocktail3 = new Cocktail
            {
                Id = testGuid3,
                Name = "TestThreeName",
                ImagePath = "ImagePathThree"
            };

            var cocktail4 = new Cocktail
            {
                Id = testGuid4,
                Name = "testFour",
                ImagePath = "ImagePathThree"
            };

            var cocktail5 = new Cocktail
            {
                Id = testGuid5,
                Name = "testFive",
                ImagePath = "ImagePathThree"
            };

            var cocktail6 = new Cocktail
            {
                Id = testGuid6,
                Name = "testSix",
                ImagePath = "ImagePathThree"
            };

            var list = new List<CocktailDto>()
            {
                new CocktailDto { Id = testGuid6, Name = "testSix"}
            };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<ICollection<Cocktail>>())).Returns(list);

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Cocktails.AddAsync(cocktail1);
                await arrangeContext.Cocktails.AddAsync(cocktail2);
                await arrangeContext.Cocktails.AddAsync(cocktail3);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new CocktailService(assertContext, mapperMock.Object, barMapperMock.Object, cocktailDetailsMapperMock.Object,
                    ingredientServiceMock.Object, cocktailIngredientServiceMock.Object);
                var result = await sut.GetFiveCocktailsAsync(1, null);
                Assert.IsInstanceOfType(result, typeof(ICollection<CocktailDto>));
                Assert.AreEqual(1, result.Count());
            }
        }

        [TestMethod]
        public async Task ReturnCorrectElementsWhen_SortIsByName()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectElementsWhen_SortIsByName));

            var mapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var barMapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var cocktailDetailsMapperMock = new Mock<IDtoMapper<Cocktail, DetailsCocktailDto>>();
            var ingredientMapperMock = new Mock<IDtoMapper<Ingredient, IngredientDto>>();

            var ingredientServiceMock = new Mock<IIngredientService>();
            var cocktailIngredientServiceMock = new Mock<ICocktailIngredientService>();


            var testGuid = Guid.NewGuid();
            var testGuid2 = Guid.NewGuid();
            var testGuid3 = Guid.NewGuid();

            var cocktail1 = new Cocktail
            {
                Id = testGuid,
                Name = "aTestOneName",
                ImagePath = "ImagePathOne"
            };

            var cocktail2 = new Cocktail
            {
                Id = testGuid2,
                Name = "zTestTwoName",
                ImagePath = "ImagePathTwo"

            };

            var cocktail3 = new Cocktail
            {
                Id = testGuid3,
                Name = "TestThreeName",
                ImagePath = "ImagePathThree"

            };

            var list = new List<CocktailDto>()
            {
                new CocktailDto{ Id = testGuid, Name = "aTestOneName" },
                new CocktailDto { Id = testGuid2, Name = "TestThreeName"},
                new CocktailDto { Id = testGuid3, Name = "zTestTwoName"},
            };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<ICollection<Cocktail>>())).Returns(list);

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Cocktails.AddAsync(cocktail1);
                await arrangeContext.Cocktails.AddAsync(cocktail2);
                await arrangeContext.Cocktails.AddAsync(cocktail3);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new CocktailService(assertContext, mapperMock.Object, barMapperMock.Object, cocktailDetailsMapperMock.Object,
                    ingredientServiceMock.Object, cocktailIngredientServiceMock.Object);
                var result = await sut.GetFiveCocktailsAsync(1, "name_desc");
                Assert.AreEqual("aTestOneName", result.ToArray()[0].Name);
                Assert.AreEqual(testGuid, result.ToArray()[0].Id);
                Assert.AreEqual("TestThreeName", result.ToArray()[1].Name);
                Assert.AreEqual(testGuid2, result.ToArray()[1].Id);
                Assert.AreEqual("zTestTwoName", result.ToArray()[2].Name);
                Assert.AreEqual(testGuid3, result.ToArray()[2].Id);
            }
        }

        [TestMethod]
        public async Task ReturnCorrectElementsWhen_SortIsByNameAsc()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectElementsWhen_SortIsByNameAsc));

            var mapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var barMapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var cocktailDetailsMapperMock = new Mock<IDtoMapper<Cocktail, DetailsCocktailDto>>();
            var ingredientMapperMock = new Mock<IDtoMapper<Ingredient, IngredientDto>>();

            var ingredientServiceMock = new Mock<IIngredientService>();
            var cocktailIngredientServiceMock = new Mock<ICocktailIngredientService>();

            var testGuid = Guid.NewGuid();
            var testGuid2 = Guid.NewGuid();
            var testGuid3 = Guid.NewGuid();

            var cocktail1 = new Cocktail
            {
                Id = testGuid,
                Name = "aTestOneName",
                ImagePath = "ImagePathOne"
            };

            var cocktail2 = new Cocktail
            {
                Id = testGuid2,
                Name = "zTestTwoName",
                ImagePath = "ImagePathTwo"

            };

            var cocktail3 = new Cocktail
            {
                Id = testGuid3,
                Name = "TestThreeName",
                ImagePath = "ImagePathThree"

            };

            var list = new List<CocktailDto>()
            {
                new CocktailDto { Id = testGuid3, Name = "zTestTwoName"},
                new CocktailDto { Id = testGuid2, Name = "TestThreeName"},
                new CocktailDto{ Id = testGuid, Name = "aTestOneName" },
            };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<ICollection<Cocktail>>())).Returns(list);

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Cocktails.AddAsync(cocktail1);
                await arrangeContext.Cocktails.AddAsync(cocktail2);
                await arrangeContext.Cocktails.AddAsync(cocktail3);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new CocktailService(assertContext, mapperMock.Object, barMapperMock.Object, cocktailDetailsMapperMock.Object,
                    ingredientServiceMock.Object, cocktailIngredientServiceMock.Object);
                var result = await sut.GetFiveCocktailsAsync(1, "Name");
                Assert.AreEqual("zTestTwoName", result.ToArray()[0].Name);
                Assert.AreEqual(testGuid3, result.ToArray()[0].Id);
                Assert.AreEqual("TestThreeName", result.ToArray()[1].Name);
                Assert.AreEqual(testGuid2, result.ToArray()[1].Id);
                Assert.AreEqual("aTestOneName", result.ToArray()[2].Name);
                Assert.AreEqual(testGuid, result.ToArray()[2].Id);
            }
        }
    }
}

