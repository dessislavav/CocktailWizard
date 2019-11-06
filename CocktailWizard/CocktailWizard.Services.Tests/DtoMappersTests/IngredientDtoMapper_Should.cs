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
    public class IngredientDtoMapper_Should
    {
        [TestMethod]
        public void MapFrom_Should_ReturnCorrectInstanceOf_IngredientDto()
        {
            //Arrange
            var sut = new IngredientDtoMapper();

            var ingredient = new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "testName"
            };

            //Act
            var result = sut.MapFrom(ingredient);

            //Assert
            Assert.IsInstanceOfType(result, typeof(IngredientDto));
        }

        [TestMethod]
        public void MapFrom_Should_CorrectlyMapIdFrom_Ingredient_To_IngredientDto()
        {
            //Arrange
            var sut = new IngredientDtoMapper();

            var ingredient = new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "testName"
            };

            //Act
            var result = sut.MapFrom(ingredient);

            //Assert
            Assert.AreEqual(result.Id, ingredient.Id);
        }

        [TestMethod]
        public void MapFrom_Should_CorrectlyMapNameFrom_Ingredient_To_IngredientDto()
        {
            //Arrange
            var sut = new IngredientDtoMapper();

            var ingredient = new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "testName"
            };

            //Act
            var result = sut.MapFrom(ingredient);

            //Assert
            Assert.AreEqual(result.Name, ingredient.Name);
        }

        [TestMethod]
        public void MapFromCollection_Should_ReturnCorrectInstanceOfCollection_IngredientDto()
        {
            //Arrange
            var sut = new IngredientDtoMapper();

            var ingredientList = new List<Ingredient>()
            {
                new Ingredient
                {
                    Id = Guid.NewGuid(),
                    Name = "testName"
                },
                new Ingredient
                {
                    Id = Guid.NewGuid(),
                    Name = "testName2"
                }
            };

            //Act
            var result = sut.MapFrom(ingredientList);

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<IngredientDto>));
        }

        [TestMethod]
        public void MapFromCollection_Should_CorrectlyMapNameFrom_Ingredient_To_IngredientDto()
        {
            //Arrange
            var sut = new IngredientDtoMapper();

            var ingredientList = new List<Ingredient>()
            {
                new Ingredient
                {
                    Id = Guid.NewGuid(),
                    Name = "testName"
                },
                new Ingredient
                {
                    Id = Guid.NewGuid(),
                    Name = "testName2"
                }
            };

            //Act
            var result = sut.MapFrom(ingredientList);

            //Assert
                Assert.AreEqual(result.Count(), 2);
        }

    }
}
