using CocktailWizard.Data.Entities;
using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Services.DtoMappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CocktailWizard.Services.Tests.DtoMappersTests
{
    [TestClass]
    public class CocktailCommentDtoMapper_Should
    {
        [TestMethod]
        public void MapFrom_Should_ReturnCorrectInstanceOf_CocktailCommentDto()
        {
            //Arrange
            var sut = new CocktailCommentDtoMapper();

            var cocktailComment = new CocktailComment
            {
                Cocktail = new Cocktail
                {
                    Id = Guid.NewGuid(),
                    Name = "testCocktail",
                    Info = "testInfo",
                    ImagePath = "testPath",
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
            var result = sut.MapFrom(cocktailComment);

            //Assert
            Assert.IsInstanceOfType(result, typeof(CocktailCommentDto));
        }

        [TestMethod]
        public void MapFrom_Should_CorrectlyMapFrom_CocktailComment_To_CocktailCommentDto()
        {
            //Arrange
            var sut = new CocktailCommentDtoMapper();

            var cocktailComment = new CocktailComment
            {
                Cocktail = new Cocktail
                {
                    Id = Guid.NewGuid(),
                    Name = "testCocktail",
                    Info = "testInfo",
                    ImagePath = "testPath",
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
            var result = sut.MapFrom(cocktailComment);

            //Assert
            Assert.AreEqual(result.CocktailId, cocktailComment.CocktailId);
            Assert.AreEqual(result.UserId, cocktailComment.UserId);
            Assert.AreEqual(result.Body, cocktailComment.Body);
            Assert.AreEqual(result.CreatedOn, cocktailComment.CreatedOn);
        }

        [TestMethod]
        public void MapFrom_Should_ReturnCorrectInstanceOfCollection_CocktailCommentCommentDto()
        {
            //Arrange
            var sut = new CocktailCommentDtoMapper();

            var cocktailComment = new List<CocktailComment>()
            {
                new CocktailComment
                {
                    Cocktail = new Cocktail
                    {
                        Id = Guid.NewGuid(),
                        Name = "testCocktail",
                        Info = "testInfo",
                        ImagePath = "testPath",
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
                new CocktailComment
                {
                    Cocktail = new Cocktail
                    {
                        Id = Guid.NewGuid(),
                        Name = "testCocktail2",
                        Info = "testInfo2",
                        ImagePath = "testPath2",
                    },
                    User = new User
                    {
                        Id = Guid.NewGuid(),
                        UserName = "testUsername2@aaa.aa",
                        Email = "testUsername2@aaa.aa"
                    },
                    Body = "testBody2",
                    CreatedOn = DateTime.MinValue,
                },
            };


            //Act
            var result = sut.MapFrom(cocktailComment);

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<CocktailCommentDto>));
        }

        [TestMethod]
        public void MapFromCollection_Should_ReturnCorrectCountCommentCocktails()
        {
            //Arrange
            var sut = new CocktailCommentDtoMapper();

            var cocktailComment = new List<CocktailComment>()
            {
                new CocktailComment
                {
                    Cocktail = new Cocktail
                    {
                        Id = Guid.NewGuid(),
                        Name = "testCocktail",
                        Info = "testInfo",
                        ImagePath = "testPath",
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
                new CocktailComment
                {
                    Cocktail = new Cocktail
                    {
                        Id = Guid.NewGuid(),
                        Name = "testCocktail2",
                        Info = "testInfo2",
                        ImagePath = "testPath2",
                    },
                    User = new User
                    {
                        Id = Guid.NewGuid(),
                        UserName = "testUsername2@aaa.aa",
                        Email = "testUsername2@aaa.aa"
                    },
                    Body = "testBody2",
                    CreatedOn = DateTime.MinValue,
                },
            };

            //Act
            var result = sut.MapFrom(cocktailComment);

            //Assert
            Assert.AreEqual(2, result.Count());
        }
    }
}
