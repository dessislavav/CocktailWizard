using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Web.Mappers;
using CocktailWizard.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CocktailWizard.Services.Tests.ViewModelMappersTests
{
    [TestClass]
    public class DetailsCocktailViewModelMapper_Should
    {
        [TestMethod]
        public void MapFrom_Should_ReturnCorrectInstanceOf_DetailsCocktailViewModel()
        {
            //Arrange
            var sut = new DetailsCocktailViewModelMapper();

            var cocktail = new DetailsCocktailDto
            {
                Id = Guid.NewGuid(),
                Name = "testName",
                Info = "testInfo",
                ImagePath = "testPath",
            };

            //Act
            var result = sut.MapFrom(cocktail);

            //Assert
            Assert.IsInstanceOfType(result, typeof(DetailsCocktailViewModel));
        }

        [TestMethod]
        public void MapFrom_Should_CorrectlyMapFrom_DetailsCocktail_To_DetailsCocktailViewModel()
        {
            //Arrange
            var sut = new DetailsCocktailViewModelMapper();

            var cocktail = new DetailsCocktailDto
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
        public void MapFrom_Should_CorrectlyMapRatingFrom_DetailsCocktailDto_To_DetailsCocktailViewModel_WhenCollectionIsNotEmpty()
        {
            //Arrange
            var sut = new DetailsCocktailViewModelMapper();

            var cocktail = new DetailsCocktailDto
            {
                Id = Guid.NewGuid(),
                Name = "testName",
                Info = "testInfo",
                ImagePath = "testPath",
                AverageRating = 4.55,
            };

            //Act
            var result = sut.MapFrom(cocktail);

            //Assert
            Assert.AreEqual(result.AverageRating, 4.55);
        }

        [TestMethod]
        public void MapFrom_Should_ReturnCorrectInstanceOfCollection_DetailsCocktailViewModel()
        {
            //Arrange
            var sut = new DetailsCocktailViewModelMapper();

            var cocktails = new List<DetailsCocktailDto>()
            {
                new DetailsCocktailDto
                {
                    Id = Guid.NewGuid(),
                    Name = "testName",
                    Info = "testInfo",
                    ImagePath = "testPath",
                },
                new DetailsCocktailDto
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
            Assert.IsInstanceOfType(result, typeof(List<DetailsCocktailViewModel>));
        }

        [TestMethod]
        public void MapFromCollection_Should_ReturnCorrectCountCocktails()
        {
            //Arrange
            var sut = new DetailsCocktailViewModelMapper();

            var cocktails = new List<DetailsCocktailDto>()
            {
                new DetailsCocktailDto
                {
                    Id = Guid.NewGuid(),
                    Name = "testName",
                    Info = "testInfo",
                    ImagePath = "testPath",
                },
                new DetailsCocktailDto
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
