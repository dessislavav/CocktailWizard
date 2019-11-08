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
    public class SearchBarDtoMapper_Should
    {
        //bez info phone i google
        [TestMethod]
        public void MapFrom_Should_ReturnCorrectInstanceOf_SearchBarDto()
        {
            //Arrange
            var sut = new SearchBarDtoMapper();

            var bar = new Bar
            {
                Id = Guid.NewGuid(),
                Name = "testName",
                ImagePath = "testPath",
                Address = "testAddress",
            };

            //Act
            var result = sut.MapFrom(bar);

            //Assert
            Assert.IsInstanceOfType(result, typeof(SearchBarDto));
        }

        [TestMethod]
        public void MapFrom_Should_CorrectlyMapFrom_Bar_To_SearchBarDto()
        {
            //Arrange
            var sut = new SearchBarDtoMapper();

            var bar = new Bar
            {
                Id = Guid.NewGuid(),
                Name = "testName",
                ImagePath = "testPath",
                Address = "testAddress",
            };

            //Act
            var result = sut.MapFrom(bar);

            //Assert
            Assert.AreEqual(result.Id, bar.Id);
            Assert.AreEqual(result.Name, bar.Name);
            Assert.AreEqual(result.ImagePath, bar.ImagePath);
            Assert.AreEqual(result.Address, bar.Address);
        }

        [TestMethod]
        public void MapFrom_Should_CorrectlyMapRatingFrom_Bar_To_SearchBarDto_WhenCollectionIsEmpty()
        {
            //Arrange
            var sut = new SearchBarDtoMapper();

            var bar = new Bar
            {
                Id = Guid.NewGuid(),
                Name = "testName",
                ImagePath = "testPath",
                Address = "testAddress",
            };

            //Act
            var result = sut.MapFrom(bar);

            //Assert
            Assert.AreEqual(result.AverageRating, 0.00);
        }

        [TestMethod]
        public void MapFrom_Should_CorrectlyMapRatingFrom_Bar_To_SearchBarDto_WhenCollectionIsNotEmpty()
        {
            //Arrange
            var sut = new SearchBarDtoMapper();

            var bar = new Bar
            {
                Id = Guid.NewGuid(),
                Name = "testName",
                ImagePath = "testPath",
                Address = "testAddress",
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
        public void MapFrom_Should_ReturnCorrectInstanceOfCollection_SearchBarDto()
        {
            //Arrange
            var sut = new SearchBarDtoMapper();

            var bars = new List<Bar>()
            {
                new Bar
                {
                    Id = Guid.NewGuid(),
                    Name = "testName",
                    ImagePath = "testPath",
                    Address = "testAddress",
                },
                new Bar
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
            Assert.IsInstanceOfType(result, typeof(List<SearchBarDto>));
        }

        [TestMethod]
        public void MapFromCollection_Should_ReturnCorrectCountBars()
        {
            //Arrange
            var sut = new SearchBarDtoMapper();

            var bars = new List<Bar>()
            {
                new Bar
                {
                    Id = Guid.NewGuid(),
                    Name = "testName",
                    ImagePath = "testPath",
                    Address = "testAddress",
                },
                new Bar
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
