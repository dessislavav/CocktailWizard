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
    public class BarCommentViewModelMapper_Should
    {
        [TestMethod]
        public void MapFrom_Should_ReturnCorrectInstanceOf_BarCommentViewModel()
        {
            //Arrange
            var sut = new BarCommentViewModelMapper();

            var barComment = new BarCommentDto
            {

                Id = Guid.NewGuid(),
                BarId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                UserName = "testUsername",
                Body = "testBody",
                CreatedOn = DateTime.MinValue,
            };

            //Act
            var result = sut.MapFrom(barComment);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BarCommentViewModel));
        }

        [TestMethod]
        public void MapFrom_Should_CorrectlyMapFrom_BarCommentDto_To_BarCommentViewModel()
        {
            //Arrange
            var sut = new BarCommentViewModelMapper();

            var barComment = new BarCommentDto
            {

                Id = Guid.NewGuid(),
                BarId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                UserName = "testUsername",
                Body = "testBody",
                CreatedOn = DateTime.MinValue,
            };

            //Act
            var result = sut.MapFrom(barComment);

            //Assert
            Assert.AreEqual(result.Id, barComment.Id);
            Assert.AreEqual(result.BarId, barComment.BarId);
            Assert.AreEqual(result.UserId, barComment.UserId);
            Assert.AreEqual(result.UserName, barComment.UserName);
            Assert.AreEqual(result.Body, barComment.Body);
            Assert.AreEqual(result.CreatedOn, barComment.CreatedOn);
        }

        [TestMethod]
        public void MapFrom_Should_ReturnCorrectInstanceOfCollection_BarCommentDto()
        {
            //Arrange
            var sut = new BarCommentViewModelMapper();

            var barComment = new List<BarCommentDto>
            {
                new BarCommentDto
                {
                Id = Guid.NewGuid(),
                BarId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                UserName = "testUsername",
                Body = "testBody",
                CreatedOn = DateTime.MinValue,
                },
                new BarCommentDto
                {
                Id = Guid.NewGuid(),
                BarId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                UserName = "testUsername2",
                Body = "testBody2",
                CreatedOn = DateTime.MinValue,
                },
            };

            //Act
            var result = sut.MapFrom(barComment);

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<BarCommentViewModel>));
        }

        [TestMethod]
        public void MapFromCollection_Should_ReturnCorrectCountCommentBars()
        {
            //Arrange
            var sut = new BarCommentViewModelMapper();

            var barComment = new List<BarCommentDto>
            {
                new BarCommentDto
                {
                Id = Guid.NewGuid(),
                BarId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                UserName = "testUsername",
                Body = "testBody",
                CreatedOn = DateTime.MinValue,
                },
                new BarCommentDto
                {
                Id = Guid.NewGuid(),
                BarId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                UserName = "testUsername2",
                Body = "testBody2",
                CreatedOn = DateTime.MinValue,
                },
            };

            //Act
            var result = sut.MapFrom(barComment);

            //Assert
            Assert.AreEqual(2, result.Count());
        }
    }
}
