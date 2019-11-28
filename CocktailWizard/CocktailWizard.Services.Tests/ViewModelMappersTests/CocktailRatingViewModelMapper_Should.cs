using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Web.Areas.Member.Models;
using CocktailWizard.Web.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CocktailWizard.Services.Tests.ViewModelMappersTests
{
    [TestClass]
    public class CocktailRatingViewModelMapper_Should
    {
        [TestMethod]
        public void MapFrom_Should_ReturnCorrectInstanceOf_CocktailRatingViewModel()
        {
            //Arrange
            var sut = new CocktailRatingViewModelMapper();

            var cocktailRating = new CocktailRatingDto
            {
                UserId = Guid.NewGuid(),
                UserName = "testUsername",
                CocktailId = Guid.NewGuid(),
                CreatedOn = DateTime.Now,
                Value = 4,
            };

            //Act
            var result = sut.MapFrom(cocktailRating);

            //Assert
            Assert.IsInstanceOfType(result, typeof(CocktailRatingViewModel));
        }

        [TestMethod]
        public void MapFrom_Should_CorrectlyMapFrom_CocktailRatingDto_To_CocktailRatingViewModel()
        {
            //Arrange
            var sut = new CocktailRatingViewModelMapper();

            var cocktailRating = new CocktailRatingDto
            {
                UserId = Guid.NewGuid(),
                UserName = "testUsername",
                CocktailId = Guid.NewGuid(),
                CreatedOn = DateTime.Now,
                Value = 4,
            };

            //Act
            var result = sut.MapFrom(cocktailRating);

            //Assert
            Assert.AreEqual(result.Value, cocktailRating.Value);
            Assert.AreEqual(result.CocktailId, cocktailRating.CocktailId);
            Assert.AreEqual(result.UserId, cocktailRating.UserId);
            Assert.AreEqual(result.UserName, cocktailRating.UserName);
            Assert.AreEqual(result.CreatedOn, cocktailRating.CreatedOn);
        }

        [TestMethod]
        public void MapFrom_Should_ReturnCorrectInstanceOfCollection_CocktailRatingDto()
        {
            //Arrange
            var sut = new CocktailRatingViewModelMapper();

            var cocktailRatings = new List<CocktailRatingDto>
            {
                new CocktailRatingDto
                {
                Value = 5,
                CocktailId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                UserName = "testUsername",
                CreatedOn = DateTime.MinValue,
                },
                new CocktailRatingDto
                {
                Value = 4,
                CocktailId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                UserName = "testUsername2",
                CreatedOn = DateTime.MinValue,
                },
            };

            //Act
            var result = sut.MapFrom(cocktailRatings);

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<CocktailRatingViewModel>));
        }

        [TestMethod]
        public void MapFromCollection_Should_ReturnCorrectCountCocktailRating()
        {
            //Arrange
            var sut = new CocktailRatingViewModelMapper();

            var cocktailRatings = new List<CocktailRatingDto>
            {
                new CocktailRatingDto
                {
                Value = 5,
                CocktailId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                UserName = "testUsername",
                CreatedOn = DateTime.MinValue,
                },
                new CocktailRatingDto
                {
                Value = 4,
                CocktailId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                UserName = "testUsername2",
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
