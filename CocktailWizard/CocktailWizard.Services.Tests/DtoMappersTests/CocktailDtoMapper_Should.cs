using CocktailWizard.Data.Entities;
using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Services.DtoMappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CocktailWizard.Services.Tests.DtoMappersTests
{
    [TestClass]
    public class CocktailDtoMapper_Should
    {
        [TestMethod]
        public void MapFrom_Should_ReturnCorrectInstanceOf_CocktailDto()
        {
            //Arrange
            var sut = new CocktailDtoMapper();

            var cocktail = new Cocktail
            {
                Id = Guid.NewGuid(),
                Name = "testName",
                Info = "testInfo",
                ImagePath = "testPath",
            };

            //Act
            var result = sut.MapFrom(cocktail);

            //Assert
            Assert.IsInstanceOfType(result, typeof(CocktailDto));
        }

        [TestMethod]
        public void MapFrom_Should_CorrectlyMapFrom_Cocktail_To_CocktailDto()
        {
            //Arrange
            var sut = new CocktailDtoMapper();

            var cocktail = new Cocktail
            {
                Id = Guid.NewGuid(),
                Name = "testName",
                Info = "testInfo",
                ImagePath = "testPath",
            };

            //Act
            var result = sut.MapFrom(cocktail);

            //Assert
            Assert.AreEqual(result.Id, cocktail.Id);
            Assert.AreEqual(result.Name, cocktail.Name);
            Assert.AreEqual(result.Info, cocktail.Info);
            Assert.AreEqual(result.ImagePath, cocktail.ImagePath);
        }

        [TestMethod]
        public void MapFrom_Should_CorrectlyMapRatingFrom_Cocktail_To_CocktailDto_WhenCollectionIsEmpty()
        {
            //Arrange
            var sut = new CocktailDtoMapper();

            var cocktail = new Cocktail
            {
                Id = Guid.NewGuid(),
                Name = "testName",
                Info = "testInfo",
                ImagePath = "testPath",
            };

            //Act
            var result = sut.MapFrom(cocktail);

            //Assert
            Assert.AreEqual(result.AverageRating, 0.00);
        }

        [TestMethod]
        public void MapFrom_Should_CorrectlyMapRatingFrom_Cocktail_To_CocktailDto_WhenCollectionIsNotEmpty()
        {
            //Arrange
            var sut = new CocktailDtoMapper();

            var cocktail = new Cocktail
            {
                Id = Guid.NewGuid(),
                Name = "testName",
                Info = "testInfo",
                ImagePath = "testPath",
                Ratings = new List<CocktailRating>()
                {
                    new CocktailRating
                    {
                        Value = 4.55,
                    }
                }
            };

            //Act
            var result = sut.MapFrom(cocktail);

            //Assert
            Assert.AreEqual(result.AverageRating, 4.55);
        }

        [TestMethod]
        public void MapFrom_Should_CorrectlyMapIngredientsFrom_Cocktail_To_CocktailDto()
        {
            //Arrange
            var sut = new CocktailDtoMapper();

            var cocktail = new Cocktail
            {
                Id = Guid.NewGuid(),
                Name = "testName",
                Info = "testInfo",
                ImagePath = "testPath",
                CocktailIngredients = new List<CocktailIngredient>()
                {
                   new CocktailIngredient()
                   {
                      Ingredient = new Ingredient
                      {
                          Id = Guid.NewGuid(),
                          Name = "djodjan"
                      },
                   },
                }
            };

            //Act
            var result = sut.MapFrom(cocktail);

            //Assert
            Assert.AreEqual(result.CocktailIngredients.First(), "djodjan");
        }

        [TestMethod]
        public void MapFrom_Should_ReturnCorrectInstanceOfCollection_CocktailDto()
        {
            //Arrange
            var sut = new CocktailDtoMapper();

            var cocktails = new List<Cocktail>()
            {
                new Cocktail
                {
                    Id = Guid.NewGuid(),
                    Name = "testName",
                    Info = "testInfo",
                    ImagePath = "testPath",
                },
                new Cocktail
                {
                    Id = Guid.NewGuid(),
                    Name = "testName2",
                    Info = "testInfo2",
                    ImagePath = "testPath2",
                }
            };

            //Act
            var result = sut.MapFrom(cocktails);

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<CocktailDto>));
        }

        [TestMethod]
        public void MapFromCollection_Should_ReturnCorrectCountCocktails()
        {
            //Arrange
            var sut = new CocktailDtoMapper();

            var cocktails = new List<Cocktail>()
            {
                new Cocktail
                {
                    Id = Guid.NewGuid(),
                    Name = "testName",
                    Info = "testInfo",
                    ImagePath = "testPath",
                },
                new Cocktail
                {
                    Id = Guid.NewGuid(),
                    Name = "testName2",
                    Info = "testInfo2",
                    ImagePath = "testPath2",
                }
            };

            //Act
            var result = sut.MapFrom(cocktails);

            //Assert
            Assert.AreEqual(2, result.Count());
        }
    }
}