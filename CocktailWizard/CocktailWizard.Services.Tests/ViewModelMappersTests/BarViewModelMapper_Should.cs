using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Web.Mappers;
using CocktailWizard.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CocktailWizard.Services.Tests.ViewModelMappersTests
{
    [TestClass]
    public class BarViewModelMapper_Should
    {
        [TestMethod]
        public void MapFrom_Should_ReturnCorrectInstanceOf_BarViewModel()
        {
            //Arrange
            var sut = new BarViewModelMapper();

            var bar = new BarDto
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
            Assert.IsInstanceOfType(result, typeof(BarViewModel));
        }

        [TestMethod]
        public void MapFrom_Should_CorrectlyMapFrom_Bar_To_BarViewModel()
        {
            //Arrange
            var sut = new BarViewModelMapper();

            var bar = new BarDto
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
        public void MapFrom_Should_CorrectlyMapRatingFrom_BarDto_To_BarViewModel_WhenCollectionIsNotEmpty()
        {
            //Arrange
            var sut = new BarViewModelMapper();

            var bar = new BarDto
            {
                Id = Guid.NewGuid(),
                Name = "testName",
                Info = "testInfo",
                ImagePath = "testPath",
                Address = "testAddress",
                GoogleMapsURL = "testMapsLink.gg",
                Phone = "111-333-666",
                AverageRating = 4.55,
            };

            //Act
            var result = sut.MapFrom(bar);

            //Assert
            Assert.AreEqual(result.AverageRating, 4.55);
        }

        [TestMethod]
        public void MapFrom_Should_ReturnCorrectInstanceOfCollection_BarViewModel()
        {
            //Arrange
            var sut = new BarViewModelMapper();

            var bars = new List<BarDto>()
            {
                new BarDto
                {
                    Id = Guid.NewGuid(),
                    Name = "testName",
                    Info = "testInfo",
                    ImagePath = "testPath",
                    Address = "testAddress",
                    GoogleMapsURL = "testMapsLink.gg",
                    Phone = "111-333-666",
                },
                new BarDto
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
            Assert.IsInstanceOfType(result, typeof(List<BarViewModel>));
        }

        [TestMethod]
        public void MapFromCollection_Should_ReturnCorrectCountBars()
        {
            //Arrange
            var sut = new BarViewModelMapper();

            var bars = new List<BarDto>()
            {
                new BarDto
                {
                    Id = Guid.NewGuid(),
                    Name = "testName",
                    Info = "testInfo",
                    ImagePath = "testPath",
                    Address = "testAddress",
                    GoogleMapsURL = "testMapsLink.gg",
                    Phone = "111-333-666",
                },
                new BarDto
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
