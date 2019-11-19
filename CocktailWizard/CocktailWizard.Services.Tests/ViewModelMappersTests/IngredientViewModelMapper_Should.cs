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
    public class IngredientViewModelMapper_Should
    {
        [TestMethod]
        public void MapFrom_Should_ReturnCorrectInstanceOf_IngredientViewModel()
        {
            //Arrange
            var sut = new IngredientViewModelMapper();

            var ingredient = new IngredientDto
            {
                Id = Guid.NewGuid(),
                Name = "testName",
            };

            //Act
            var result = sut.MapFrom(ingredient);

            //Assert
            Assert.IsInstanceOfType(result, typeof(IngredientViewModel));
        }

        [TestMethod]
        public void MapFrom_Should_CorrectlyMapFrom_Ingredient_To_IngredientViewModel()
        {
            //Arrange
            var sut = new IngredientViewModelMapper();

            var ingredient = new IngredientDto
            {
                Id = Guid.NewGuid(),
                Name = "testName",
            };

            //Act
            var result = sut.MapFrom(ingredient);

            //Assert
            Assert.AreEqual(result.Id, ingredient.Id);
            Assert.AreEqual(result.Name, ingredient.Name);
        }

        [TestMethod]
        public void MapFrom_Should_ReturnCorrectInstanceOfCollection_IngredientViewModel()
        {
            //Arrange
            var sut = new IngredientViewModelMapper();

            var ingredients = new List<IngredientDto>()
            {
                new IngredientDto
                {
                    Id = Guid.NewGuid(),
                    Name = "testName",
                },
                new IngredientDto
                {
                    Id = Guid.NewGuid(),
                    Name = "testName2",
                }
            };

            //Act
            var result = sut.MapFrom(ingredients);

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<IngredientViewModel>));
        }

        [TestMethod]
        public void MapFromCollection_Should_ReturnCorrectCountBars()
        {
            //Arrange
            var sut = new IngredientViewModelMapper();

            var ingredients = new List<IngredientDto>()
            {
                new IngredientDto
                {
                    Id = Guid.NewGuid(),
                    Name = "testName",
                },
                new IngredientDto
                {
                    Id = Guid.NewGuid(),
                    Name = "testName2",
                }
            };

            //Act
            var result = sut.MapFrom(ingredients);

            //Assert
            Assert.AreEqual(2, result.Count());
        }
    }
}
