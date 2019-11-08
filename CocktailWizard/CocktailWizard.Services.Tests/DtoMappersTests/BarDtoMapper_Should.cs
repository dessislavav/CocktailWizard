using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.DtoMappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CocktailWizard.Services.Tests.DtoMappersTests
{
    [TestClass]
    public class BarDtoMapper_Should
    {
        [TestMethod]
        public void MapFrom_Should_ReturnCorrectInstanceOf_BarDto()
        {
            //Arrange
            var sut = new BarDtoMapper();

            var bar = new Bar
            {
                Id = Guid.NewGuid(),
                Name = "testName",
                Info = "testInfo",
                ImagePath = "testPath",
                Address = "testAddress",
                GoogleMapsURL = "testMapsLink.gg",
                Phone = "111-333-666",
            };

            //Act
            var result = sut.MapFrom(bar);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BarDto));
        }

        [TestMethod]
        public void MapFrom_Should_CorrectlyMapFrom_Bar_To_BarDto()
        {
            //Arrange
            var sut = new BarDtoMapper();

            var bar = new Bar
            {
                Id = Guid.NewGuid(),
                Name = "testName",
                Info = "testInfo",
                ImagePath = "testPath",
                Address = "testAddress",
                GoogleMapsURL = "testMapsLink.gg",
                Phone = "111-333-666",
            };

            //Act
            var result = sut.MapFrom(bar);

            //Assert
            Assert.AreEqual(result.Id, bar.Id);
            Assert.AreEqual(result.Name, bar.Name);
            Assert.AreEqual(result.Info, bar.Info);
            Assert.AreEqual(result.ImagePath, bar.ImagePath);
            Assert.AreEqual(result.Address, bar.Address);
            Assert.AreEqual(result.GoogleMapsURL, bar.GoogleMapsURL);
            Assert.AreEqual(result.Phone, bar.Phone);
        }

        [TestMethod]
        public void MapFrom_Should_CorrectlyMapRatingFrom_Bar_To_BarDto_WhenCollectionIsEmpty()
        {
            //Arrange
            var sut = new BarDtoMapper();

            var bar = new Bar
            {
                Id = Guid.NewGuid(),
                Name = "testName",
                Info = "testInfo",
                ImagePath = "testPath",
                Address = "testAddress",
                GoogleMapsURL = "testMapsLink.gg",
                Phone = "111-333-666",
            };

            //Act
            var result = sut.MapFrom(bar);

            //Assert
            Assert.AreEqual(result.AverageRating, 0.00);
        }

        [TestMethod]
        public void MapFrom_Should_CorrectlyMapRatingFrom_Bar_To_BarDto_WhenCollectionIsNotEmpty()
        {
            //Arrange
            var sut = new BarDtoMapper();

            var bar = new Bar
            {
                Id = Guid.NewGuid(),
                Name = "testName",
                Info = "testInfo",
                ImagePath = "testPath",
                Address = "testAddress",
                GoogleMapsURL = "testMapsLink.gg",
                Phone = "111-333-666",
                Ratings = new List<BarRating>()
                {
                    new BarRating
                    {
                        Value = 4.55,
                    }
                }
            };

            //Act
            var result = sut.MapFrom(bar);

            //Assert
            Assert.AreEqual(result.AverageRating, 4.55);
        }

        [TestMethod]
        public void MapFrom_Should_ReturnCorrectInstanceOfCollection_BarDto()
        {
            //Arrange
            var sut = new BarDtoMapper();

            var bars = new List<Bar>()
            {
                new Bar
                {
                    Id = Guid.NewGuid(),
                    Name = "testName",
                    Info = "testInfo",
                    ImagePath = "testPath",
                    Address = "testAddress",
                    GoogleMapsURL = "testMapsLink.gg",
                    Phone = "111-333-666",
                },
                new Bar
                {
                    Id = Guid.NewGuid(),
                    Name = "testName2",
                    Info = "testInfo2",
                    ImagePath = "testPath2",
                    Address = "testAddress2",
                    GoogleMapsURL = "testMapsLink2.gg",
                    Phone = "111-333-6666",
                }
            };

            //Act
            var result = sut.MapFrom(bars);

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<BarDto>));
        }

        [TestMethod]
        public void MapFromCollection_Should_ReturnCorrectCountBars()
        {
            //Arrange
            var sut = new BarDtoMapper();

            var bars = new List<Bar>()
            {
                new Bar
                {
                    Id = Guid.NewGuid(),
                    Name = "testName",
                    Info = "testInfo",
                    ImagePath = "testPath",
                    Address = "testAddress",
                    GoogleMapsURL = "testMapsLink.gg",
                    Phone = "111-333-666",
                },
                new Bar
                {
                    Id = Guid.NewGuid(),
                    Name = "testName2",
                    Info = "testInfo2",
                    ImagePath = "testPath2",
                    Address = "testAddress2",
                    GoogleMapsURL = "testMapsLink2.gg",
                    Phone = "111-333-6666",
                }
            };

            //Act
            var result = sut.MapFrom(bars);

            //Assert
            Assert.AreEqual(2, result.Count());
        }
    }
}