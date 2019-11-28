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
    public class SearchBarViewModelMapper_Should
    {
        [TestMethod]
        public void MapFrom_Should_ReturnCorrectInstanceOf_BarViewModel()
        {
            //Arrange
            var sut = new SearchBarViewModelMapper();

            var bar = new SearchBarDto
            {
                Id = Guid.NewGuid(),
                Name = "testName",
                ImagePath = "testPath",
                Address = "testAddress",
                AverageRating = 5,
            };

            //Act
            var result = sut.MapFrom(bar);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BarViewModel));
        }

        [TestMethod]
        public void MapFrom_Should_CorrectlyMapFrom_SearchBar_To_BarViewModel()
        {
            //Arrange
            var sut = new SearchBarViewModelMapper();

            var bar = new SearchBarDto
            {
                Id = Guid.NewGuid(),
                Name = "testName",
                ImagePath = "testPath",
                Address = "testAddress",
                AverageRating = 5,
            };

            //Act
            var result = sut.MapFrom(bar);

            //Assert
            Assert.AreEqual(result.Id, bar.Id);
            Assert.AreEqual(result.Name, bar.Name);
            Assert.AreEqual(result.ImagePath, bar.ImagePath);
            Assert.AreEqual(result.Address, bar.Address);
            Assert.AreEqual(result.AverageRating, bar.AverageRating);
        }

        [TestMethod]
        public void MapFrom_Should_ReturnCorrectInstanceOfCollection_BarViewModel()
        {
            //Arrange
            var sut = new SearchBarViewModelMapper();

            var bars = new List<SearchBarDto>()
            {
                new SearchBarDto
                {
                    Id = Guid.NewGuid(),
                    Name = "testName",
                    ImagePath = "testPath",
                    Address = "testAddress",
                },
                new SearchBarDto
                {
                    Id = Guid.NewGuid(),
                    Name = "testName2",
                    ImagePath = "testPath2",
                    Address = "testAddress2",
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
            var sut = new SearchBarViewModelMapper();

            var bars = new List<SearchBarDto>()
            {
                new SearchBarDto
                {
                    Id = Guid.NewGuid(),
                    Name = "testName",
                    ImagePath = "testPath",
                    Address = "testAddress",
                },
                new SearchBarDto
                {
                    Id = Guid.NewGuid(),
                    Name = "testName2",
                    ImagePath = "testPath2",
                    Address = "testAddress2",
                }
            };

            //Act
            var result = sut.MapFrom(bars);

            //Assert
            Assert.AreEqual(2, result.Count());
        }
    }
}