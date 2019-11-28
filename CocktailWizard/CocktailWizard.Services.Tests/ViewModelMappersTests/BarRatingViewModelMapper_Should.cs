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
    public class BarRatingViewModelMapper_Should
    {
        [TestMethod]
        public void MapFrom_Should_ReturnCorrectInstanceOf_BarRatingViewModel()
        {
            //Arrange
            var sut = new BarRatingViewModelMapper();

            var barRating = new BarRatingDto
            {
                UserId = Guid.NewGuid(),
                UserName = "testUsername",
                BarId = Guid.NewGuid(),
                CreatedOn = DateTime.Now,
                Value = 4,
            };

            //Act
            var result = sut.MapFrom(barRating);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BarRatingViewModel));
        }

        [TestMethod]
        public void MapFrom_Should_CorrectlyMapFrom_BarRatingDto_To_BarRatingViewModel()
        {
            //Arrange
            var sut = new BarRatingViewModelMapper();

            var barRating = new BarRatingDto
            {
                UserId = Guid.NewGuid(),
                UserName = "testUsername",
                BarId = Guid.NewGuid(),
                CreatedOn = DateTime.Now,
                Value = 4,
            };

            //Act
            var result = sut.MapFrom(barRating);

            //Assert
            Assert.AreEqual(result.Value, barRating.Value);
            Assert.AreEqual(result.BarId, barRating.BarId);
            Assert.AreEqual(result.UserId, barRating.UserId);
            Assert.AreEqual(result.UserName, barRating.UserName);
            Assert.AreEqual(result.CreatedOn, barRating.CreatedOn);
        }

        [TestMethod]
        public void MapFrom_Should_ReturnCorrectInstanceOfCollection_BarRatingDto()
        {
            //Arrange
            var sut = new BarRatingViewModelMapper();

            var barRating = new List<BarRatingDto>
            {
                new BarRatingDto
                {
                Value = 5,
                BarId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                UserName = "testUsername",
                CreatedOn = DateTime.MinValue,
                },
                new BarRatingDto
                {
                Value = 4,
                BarId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                UserName = "testUsername2",
                CreatedOn = DateTime.MinValue,
                },
            };

            //Act
            var result = sut.MapFrom(barRating);

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<BarRatingViewModel>));
        }

        [TestMethod]
        public void MapFromCollection_Should_ReturnCorrectCountBarRatings()
        {
            //Arrange
            var sut = new BarRatingViewModelMapper();

            var barRating = new List<BarRatingDto>
            {
                new BarRatingDto
                {
                Value = 5,
                BarId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                UserName = "testUsername",
                CreatedOn = DateTime.MinValue,
                },
                new BarRatingDto
                {
                Value = 4,
                BarId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                UserName = "testUsername2",
                CreatedOn = DateTime.MinValue,
                },
            };

            //Act
            var result = sut.MapFrom(barRating);

            //Assert
            Assert.AreEqual(2, result.Count());
        }
    }
}
