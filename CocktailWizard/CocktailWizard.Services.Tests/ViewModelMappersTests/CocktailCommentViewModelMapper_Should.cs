using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Web.Areas.Member.Models;
using CocktailWizard.Web.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CocktailWizard.Services.Tests.ViewModelMappersTests
{
    [TestClass]
    public class CocktailCommentViewModelMapper_Should
    {
        [TestMethod]
        public void MapFrom_Should_ReturnCorrectInstanceOf_CocktailCommentViewModel()
        {
            //Arrange
            var sut = new CocktailCommentViewModelMapper();

            var cocktailComment = new CocktailCommentDto
            {

                Id = Guid.NewGuid(),
                CocktailId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                UserName = "testUsername",
                Body = "testBody",
                CreatedOn = DateTime.MinValue,
            };

            //Act
            var result = sut.MapFrom(cocktailComment);

            //Assert
            Assert.IsInstanceOfType(result, typeof(CocktailCommentViewModel));
        }

        [TestMethod]
        public void MapFrom_Should_CorrectlyMapFrom_CocktailCommentDto_To_CocktailCommentViewModel()
        {
            //Arrange
            var sut = new CocktailCommentViewModelMapper();

            var cocktailComment = new CocktailCommentDto
            {

                Id = Guid.NewGuid(),
                CocktailId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                UserName = "testUsername",
                Body = "testBody",
                CreatedOn = DateTime.MinValue,
            };

            //Act
            var result = sut.MapFrom(cocktailComment);

            //Assert
            Assert.AreEqual(result.Id, cocktailComment.Id);
            Assert.AreEqual(result.CocktailId, cocktailComment.CocktailId);
            Assert.AreEqual(result.UserId, cocktailComment.UserId);
            Assert.AreEqual(result.UserName, cocktailComment.UserName);
            Assert.AreEqual(result.Body, cocktailComment.Body);
            Assert.AreEqual(result.CreatedOn, cocktailComment.CreatedOn);
        }

        [TestMethod]
        public void MapFrom_Should_ReturnCorrectInstanceOfCollection_CocktailCommentDto()
        {
            //Arrange
            var sut = new CocktailCommentViewModelMapper();

            var cocktailComments = new List<CocktailCommentDto>
            {
                new CocktailCommentDto
                {
                Id = Guid.NewGuid(),
                CocktailId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                UserName = "testUsername",
                Body = "testBody",
                CreatedOn = DateTime.MinValue,
                },
                new CocktailCommentDto
                {
                Id = Guid.NewGuid(),
                CocktailId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                UserName = "testUsername2",
                Body = "testBody2",
                CreatedOn = DateTime.MinValue,
                },
            };

            //Act
            var result = sut.MapFrom(cocktailComments);

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<CocktailCommentViewModel>));
        }

        [TestMethod]
        public void MapFromCollection_Should_ReturnCorrectCountCommentCocktails()
        {
            //Arrange
            var sut = new CocktailCommentViewModelMapper();

            var cocktailComments = new List<CocktailCommentDto>
            {
                new CocktailCommentDto
                {
                Id = Guid.NewGuid(),
                CocktailId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                UserName = "testUsername",
                Body = "testBody",
                CreatedOn = DateTime.MinValue,
                },
                new CocktailCommentDto
                {
                Id = Guid.NewGuid(),
                CocktailId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                UserName = "testUsername2",
                Body = "testBody2",
                CreatedOn = DateTime.MinValue,
                },
            };

            //Act
            var result = sut.MapFrom(cocktailComments);

            //Assert
            Assert.AreEqual(2, result.Count());
        }
    }
}
