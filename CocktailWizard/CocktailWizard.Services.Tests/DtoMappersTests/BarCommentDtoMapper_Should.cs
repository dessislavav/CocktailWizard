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
    public class BarCommentDtoMapper_Should
    {
        [TestMethod]
        public void MapFrom_Should_ReturnCorrectInstanceOf_BarDto()
        {
            //Arrange
            var sut = new BarCommentDtoMapper();

            var barComment = new BarComment
            {
                Bar = new Bar 
                { 
                    Id = Guid.NewGuid(),
                    Name = "testBar",
                    Info = "testInfo",
                    ImagePath = "testPath",
                    Address = "testAddress",
                    GoogleMapsURL = "GoogleMapsURL",
                    Phone = "111-333-666" 
                },
                User = new User {
                    Id = Guid.NewGuid(),
                    UserName = "testUsername@aaa.aa",
                    Email = "testUsername@aaa.aa" 
                },
                Body = "testBody",
                CreatedOn = DateTime.MinValue,
            };

            //Act
            var result = sut.MapFrom(barComment);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BarCommentDto));
        }

        [TestMethod]
        public void MapFrom_Should_CorrectlyMapFrom_Bar_To_BarCommentDto()
        {
            //Arrange
            var sut = new BarCommentDtoMapper();

            var barComment = new BarComment
            {
                Bar = new Bar 
                {
                    Id = Guid.NewGuid(),
                    Name = "testBar",
                    Info = "testInfo",
                    ImagePath = "testPath",
                    Address = "testAddress",
                    GoogleMapsURL = "GoogleMapsURL",
                    Phone = "111-333-666" 
                },
                User = new User 
                { 
                    Id = Guid.NewGuid(),
                    UserName = "testUsername@aaa.aa",
                    Email = "testUsername@aaa.aa" 
                },
                Body = "testBody",
                CreatedOn = DateTime.MinValue,
            };

            //Act
            var result = sut.MapFrom(barComment);

            //Assert
            Assert.AreEqual(result.BarId, barComment.BarId);
            Assert.AreEqual(result.UserId, barComment.UserId);
            //Assert.AreEqual(result.UserName, barComment.User.Email.Split('@')[0]);
            Assert.AreEqual(result.Body, barComment.Body);
            Assert.AreEqual(result.CreatedOn, barComment.CreatedOn);
        }

        [TestMethod]
        public void MapFrom_Should_ReturnCorrectInstanceOfCollection_BarCommentDto()
        {
            //Arrange
            var sut = new BarCommentDtoMapper();

            var barComment = new List<BarComment>()
            {
                new BarComment
                {
                Bar = new Bar 
                { 
                    Id = Guid.NewGuid(),
                    Name = "testBar", 
                    Info = "testInfo",
                    ImagePath = "testPath"
                    , Address = "testAddress",
                    GoogleMapsURL = "GoogleMapsURL",
                    Phone = "111-333-666" 
                },
                User = new User 
                { 
                    Id = Guid.NewGuid(),
                    UserName = "testUsername@aaa.aa",
                    Email = "testUsername@aaa.aa" 
                },
                Body = "testBody",
                CreatedOn = DateTime.MinValue,
                },
                new BarComment
                {
                Bar = new Bar 
                { 
                    Id = Guid.NewGuid(),
                    Name = "testBar2",
                    Info = "testInfo2",
                    ImagePath = "testPath2",
                    Address = "testAddress2",
                    GoogleMapsURL = "GoogleMapsURL2",
                    Phone = "111-333-6662" 
                },
                User = new User 
                { 
                    Id = Guid.NewGuid(),
                    UserName = "testUsername2@aaa.aa",
                    Email = "testUsername2@aaa.aa" 
                },
                Body = "testBody2",
                CreatedOn = DateTime.MinValue,
                }
            };


            //Act
            var result = sut.MapFrom(barComment);

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<BarCommentDto>));
        }

        [TestMethod]
        public void MapFromCollection_Should_ReturnCorrectCountCommentBars()
        {
            //Arrange
            var sut = new BarCommentDtoMapper();

            var barComments = new List<BarComment>()
            {
                new BarComment
                {
                Bar = new Bar 
                { 
                    Id = Guid.NewGuid(),
                    Name = "testBar", 
                    Info = "testInfo",
                    ImagePath = "testPath",
                    Address = "testAddress",
                    GoogleMapsURL = "GoogleMapsURL",
                    Phone = "111-333-666" 
                },
                User = new User 
                {
                    Id = Guid.NewGuid(),
                    UserName = "testUsername@aaa.aa",
                    Email = "testUsername@aaa.aa"
                },
                Body = "testBody",
                CreatedOn = DateTime.MinValue,
                },
                new BarComment
                {
                Bar = new Bar 
                { 
                    Id = Guid.NewGuid(), 
                    Name = "testBar2", 
                    Info = "testInfo2", 
                    ImagePath = "testPath2", 
                    Address = "testAddress2", 
                    GoogleMapsURL = "GoogleMapsURL2", 
                    Phone = "111-333-6662" 
                },
                User = new User
                { 
                    Id = Guid.NewGuid(),
                    UserName = "testUsername2@aaa.aa",
                    Email = "testUsername2@aaa.aa" 
                },
                Body = "testBody2",
                CreatedOn = DateTime.MinValue,
                }
            };

            //Act
            var result = sut.MapFrom(barComments);

            //Assert
            Assert.AreEqual(2, result.Count());
        }
    }
}
