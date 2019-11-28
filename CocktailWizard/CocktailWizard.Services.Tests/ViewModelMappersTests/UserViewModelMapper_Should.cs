using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Web.Areas.Manager.Models;
using CocktailWizard.Web.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CocktailWizard.Services.Tests.ViewModelMappersTests
{
    [TestClass]
    public class UserViewModelMapper_Should
    {
        [TestMethod]
        public void MapFrom_Should_ReturnCorrectInstanceOf_UserViewModel()
        {
            //Arrange
            var sut = new UserViewModelMapper();

            var user = new UserDto
            {
                Id = Guid.NewGuid(),
                UserName = "testName",
                Email = "testEmail@test.test",
                CreatedOn = DateTime.Now,
                IsBanned = false,
            };

            //Act
            var result = sut.MapFrom(user);

            //Assert
            Assert.IsInstanceOfType(result, typeof(UserViewModel));
        }

        [TestMethod]
        public void MapFrom_Should_CorrectlyMapFrom_User_To_UserViewModel()
        {
            //Arrange
            var sut = new UserViewModelMapper();

            var user = new UserDto
            {
                Id = Guid.NewGuid(),
                UserName = "testName",
                Email = "testEmail@test.test",
                CreatedOn = DateTime.Now,
                IsBanned = false,
            };

            //Act
            var result = sut.MapFrom(user);

            //Assert
            Assert.AreEqual(result.Id, user.Id);
            Assert.AreEqual(result.UserName, user.UserName);
            Assert.AreEqual(result.Email, user.Email);
            Assert.AreEqual(result.CreatedOn, user.CreatedOn);
            Assert.AreEqual(result.IsBanned, user.IsBanned);
        }

        [TestMethod]
        public void MapFrom_Should_ReturnCorrectInstanceOfCollection_UserViewModel()
        {
            //Arrange
            var sut = new UserViewModelMapper();

            var users = new List<UserDto>()
            {
                new UserDto
                {
                    Id = Guid.NewGuid(),
                    UserName = "testName",
                    Email = "testEmail@test.test",
                    CreatedOn = DateTime.Now,
                    IsBanned = false,
                },
                new UserDto
                {
                    Id = Guid.NewGuid(),
                    UserName = "testName2",
                    Email = "testEmail2@test.test",
                    CreatedOn = DateTime.Now,
                    IsBanned = false,
                }
            };

            //Act
            var result = sut.MapFrom(users);

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<UserViewModel>));
        }

        [TestMethod]
        public void MapFromCollection_Should_ReturnCorrectCountUser()
        {
            //Arrange
            var sut = new UserViewModelMapper();

            var users = new List<UserDto>()
            {
                new UserDto
                {
                    Id = Guid.NewGuid(),
                    UserName = "testName",
                    Email = "testEmail@test.test",
                    CreatedOn = DateTime.Now,
                    IsBanned = false,
                },
                new UserDto
                {
                    Id = Guid.NewGuid(),
                    UserName = "testName2",
                    Email = "testEmail2@test.test",
                    CreatedOn = DateTime.Now,
                    IsBanned = false,
                }
            };

            //Act
            var result = sut.MapFrom(users);

            //Assert
            Assert.AreEqual(2, result.Count());
        }
    }
}
