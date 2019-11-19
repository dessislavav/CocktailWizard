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
    public class CocktailViewModelMapper_Should
    {
        [TestMethod]
        public void MapFrom_Should_ReturnCorrectInstanceOf_CocktailViewModel()
        {
            //Arrange
            var sut = new CocktailViewModelMapper();

            var cocktail = new CocktailDto
            {
                Id = Guid.NewGuid(),
                Name = "testName",
                Info = "testInfo",
                ImagePath = "testPath",
            };

            //Act
            var result = sut.MapFrom(cocktail);

            //Assert
            Assert.IsInstanceOfType(result, typeof(CocktailViewModel));
        }

        [TestMethod]
        public void MapFrom_Should_CorrectlyMapFrom_Cocktail_To_CocktailViewModel()
        {
            //Arrange
            var sut = new CocktailViewModelMapper();

            var cocktail = new CocktailDto
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
        public void MapFrom_Should_CorrectlyMapRatingFrom_CocktailDto_To_CocktailViewModel_WhenCollectionIsNotEmpty()
        {
            //Arrange
            var sut = new CocktailViewModelMapper();

            var cocktail = new CocktailDto
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
        public void MapFrom_Should_ReturnCorrectInstanceOfCollection_CocktailViewModel()
        {
            //Arrange
            var sut = new CocktailViewModelMapper();

            var cocktails = new List<CocktailDto>()
            {
                new CocktailDto
                {
                    Id = Guid.NewGuid(),
                    Name = "testName",
                    Info = "testInfo",
                    ImagePath = "testPath",
                },
                new CocktailDto
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
            Assert.IsInstanceOfType(result, typeof(List<CocktailViewModel>));
        }

        [TestMethod]
        public void MapFromCollection_Should_ReturnCorrectCountCocktails()
        {
            //Arrange
            var sut = new CocktailViewModelMapper();

            var cocktails = new List<CocktailDto>()
            {
                new CocktailDto
                {
                    Id = Guid.NewGuid(),
                    Name = "testName",
                    Info = "testInfo",
                    ImagePath = "testPath",
                },
                new CocktailDto
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
