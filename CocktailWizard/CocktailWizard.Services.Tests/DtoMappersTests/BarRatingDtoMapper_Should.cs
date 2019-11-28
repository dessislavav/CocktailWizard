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
    public class BarRatingDtoMapper_Should
    {
        [TestMethod]
        public void MapFrom_Should_ReturnCorrectInstanceOf_BarRatingDto()
        {
            //Arrange
            var sut = new BarRatingDtoMapper();

            var barRating = new BarRating
            {
                Bar = new Bar
                {
                    Id = Guid.NewGuid(),
                    Name = "testBar",
                    Info = "testInfo",
                    ImagePath = "testPath",
                    Address = "testAddress",
                    GoogleMapsURL = "GoogleMapsURL",
                    Phone = "111-333-666"
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
            var result = sut.MapFrom(barRating);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BarRatingDto));
        }

        [TestMethod]
        public void MapFrom_Should_CorrectlyMapFrom_BarRating_To_BarRatingDto()
        {
            //Arrange
            var sut = new BarRatingDtoMapper();

            var barRating = new BarRating
            {
                Bar = new Bar
                {
                    Id = Guid.NewGuid(),
                    Name = "testBar",
                    Info = "testInfo",
                    ImagePath = "testPath",
                    Address = "testAddress",
                    GoogleMapsURL = "GoogleMapsURL",
                    Phone = "111-333-666"
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
            var result = sut.MapFrom(barRating);

            //Assert
            Assert.AreEqual(result.BarId, barRating.BarId);
            Assert.AreEqual(result.UserId, barRating.UserId);
            Assert.AreEqual(result.Value, barRating.Value);
            Assert.AreEqual(result.CreatedOn, barRating.CreatedOn);
        }

        [TestMethod]
        public void MapFrom_Should_ReturnCorrectInstanceOfCollection_BarRatingDto()
        {
            //Arrange
            var sut = new BarRatingDtoMapper();

            var barComment = new List<BarRating>()
            {
                new BarRating
                {
                    Bar = new Bar
                    {
                        Id = Guid.NewGuid(),
                        Name = "testBar",
                        Info = "testInfo",
                        ImagePath = "testPath",
                        Address = "testAddress",
                        GoogleMapsURL = "GoogleMapsURL",
                        Phone = "111-333-666"
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
                new BarRating
                {
                    Bar = new Bar
                    {
                        Id = Guid.NewGuid(),
                        Name = "testBar2",
                        Info = "testInfo2",
                        ImagePath = "testPath2",
                        Address = "testAddress2",
                        GoogleMapsURL = "GoogleMapsURL2",
                        Phone = "111-333-6662"
                    },
                    User = new User
                    {
                        Id = Guid.NewGuid(),
                        UserName = "testUsername2@aaa.aa",
                        Email = "testUsername2@aaa.aa"
                    },
                    Value = 5,
                    CreatedOn = DateTime.MinValue,
                },
            };


            //Act
            var result = sut.MapFrom(barComment);

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<BarRatingDto>));
        }

        [TestMethod]
        public void MapFromCollection_Should_ReturnCorrectCountBarRatingBars()
        {
            //Arrange
            var sut = new BarRatingDtoMapper();

            var barRating = new List<BarRating>()
            {
                new BarRating
                {
                    Bar = new Bar
                    {
                        Id = Guid.NewGuid(),
                        Name = "testBar",
                        Info = "testInfo",
                        ImagePath = "testPath",
                        Address = "testAddress",
                        GoogleMapsURL = "GoogleMapsURL",
                        Phone = "111-333-666"
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
                new BarRating
                {
                    Bar = new Bar
                    {
                        Id = Guid.NewGuid(),
                        Name = "testBar2",
                        Info = "testInfo2",
                        ImagePath = "testPath2",
                        Address = "testAddress2",
                        GoogleMapsURL = "GoogleMapsURL2",
                        Phone = "111-333-6662"
                    },
                    User = new User
                    {
                        Id = Guid.NewGuid(),
                        UserName = "testUsername2@aaa.aa",
                        Email = "testUsername2@aaa.aa"
                    },
                    Value = 5,
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
