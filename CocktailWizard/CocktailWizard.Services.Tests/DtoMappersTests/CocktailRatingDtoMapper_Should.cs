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
    public class CocktailRatingDtoMapper_Should
    {
        [TestMethod]
        public void MapFrom_Should_ReturnCorrectInstanceOf_CocktailDto()
        {
            //Arrange
            var sut = new CocktailRatingDtoMapper();

            var cocktailRating = new CocktailRating
            {
                Cocktail = new Cocktail
                {
                    Id = Guid.NewGuid(),
                    Name = "testCocktail",
                    Info = "testInfo",
                    ImagePath = "testPath",
                },
                User = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "testUsername@aaa.aa",
                    Email = "testUsername@aaa.aa"
                },
                Value = 5,
                CreatedOn = DateTime.MinValue,
            };

            //Act
            var result = sut.MapFrom(cocktailRating);

            //Assert
            Assert.IsInstanceOfType(result, typeof(CocktailRatingDto));
        }

        [TestMethod]
        public void MapFrom_Should_CorrectlyMapFrom_Cocktail_To_CocktailRatingDto()
        {
            //Arrange
            var sut = new CocktailRatingDtoMapper();

            var cocktailRating = new CocktailRating
            {
                Cocktail = new Cocktail
                {
                    Id = Guid.NewGuid(),
                    Name = "testCocktail",
                    Info = "testInfo",
                    ImagePath = "testPath",
                },
                User = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "testUsername@aaa.aa",
                    Email = "testUsername@aaa.aa"
                },
                Value = 5,
                CreatedOn = DateTime.MinValue,
            };

            //Act
            var result = sut.MapFrom(cocktailRating);

            //Assert
            Assert.AreEqual(result.CocktailId, cocktailRating.CocktailId);
            Assert.AreEqual(result.UserId, cocktailRating.UserId);
            Assert.AreEqual(result.Value, cocktailRating.Value);
            Assert.AreEqual(result.CreatedOn, cocktailRating.CreatedOn);
        }

        [TestMethod]
        public void MapFrom_Should_ReturnCorrectInstanceOfCollection_CocktailRatingDto()
        {
            //Arrange
            var sut = new CocktailRatingDtoMapper();

            var cocktailRatings = new List<CocktailRating>()
            {
                new CocktailRating
            {
                     Cocktail = new Cocktail
                     {
                         Id = Guid.NewGuid(),
                         Name = "testCocktail",
                         Info = "testInfo",
                         ImagePath = "testPath",
                     },
                     User = new User
                     {
                         Id = Guid.NewGuid(),
                         UserName = "testUsername@aaa.aa",
                         Email = "testUsername@aaa.aa"
                     },
                     Value = 5,
                     CreatedOn = DateTime.MinValue,
            },
                new CocktailRating
            {
                    Cocktail = new Cocktail
                    {
                        Id = Guid.NewGuid(),
                        Name = "testCocktail",
                        Info = "testInfo",
                        ImagePath = "testPath",
                    },
                    User = new User
                    {
                        Id = Guid.NewGuid(),
                        UserName = "testUsername@aaa.aa",
                        Email = "testUsername@aaa.aa"
                    },
                    Value = 5,
                    CreatedOn = DateTime.MinValue,
                },
            };


            //Act
            var result = sut.MapFrom(cocktailRatings);

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<CocktailRatingDto>));
        }

        [TestMethod]
        public void MapFromCollection_Should_ReturnCorrectCountCocktailRatings()
        {
            //Arrange
            var sut = new CocktailRatingDtoMapper();

            var cocktailRatings = new List<CocktailRating>()
            {
                new CocktailRating
            {
                     Cocktail = new Cocktail
                     {
                         Id = Guid.NewGuid(),
                         Name = "testCocktail",
                         Info = "testInfo",
                         ImagePath = "testPath",
                     },
                     User = new User
                     {
                         Id = Guid.NewGuid(),
                         UserName = "testUsername@aaa.aa",
                         Email = "testUsername@aaa.aa"
                     },
                     Value = 5,
                     CreatedOn = DateTime.MinValue,
            },
                new CocktailRating
            {
                    Cocktail = new Cocktail
                    {
                        Id = Guid.NewGuid(),
                        Name = "testCocktail",
                        Info = "testInfo",
                        ImagePath = "testPath",
                    },
                    User = new User
                    {
                        Id = Guid.NewGuid(),
                        UserName = "testUsername@aaa.aa",
                        Email = "testUsername@aaa.aa"
                    },
                    Value = 5,
                    CreatedOn = DateTime.MinValue,
                },
            };

            //Act
            var result = sut.MapFrom(cocktailRatings);

            //Assert
            Assert.AreEqual(2, result.Count());
        }
    }
}
