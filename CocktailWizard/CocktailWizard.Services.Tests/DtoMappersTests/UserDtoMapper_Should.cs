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
    public class UserDtoMapper_Should
    {
        [TestMethod]
        public void MapFrom_Should_ReturnCorrectInstanceOf_UserDto()
        {
            //Arrange
            var sut = new UserDtoMapper();

            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = "testUsername2@aaa.aa",
                Email = "testUsername2@aaa.aa"
            };

            //Act
            var result = sut.MapFrom(user);

            //Assert
            Assert.IsInstanceOfType(result, typeof(UserDto));
        }

        [TestMethod]
        public void MapFrom_Should_CorrectlyMapFrom_User_To_UserDto()
        {
            //Arrange
            var sut = new UserDtoMapper();

            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = "testUsername2@aaa.aa",
                Email = "testUsername2@aaa.aa"
            };
            //Act
            var result = sut.MapFrom(user);

            //Assert
            Assert.AreEqual(result.Id, user.Id);
            Assert.AreEqual(result.UserName, user.UserName);
            Assert.AreEqual(result.Email, user.Email);
        }

        [TestMethod]
        public void MapFrom_Should_ReturnCorrectInstanceOfCollection_UsersDto()
        {
            //Arrange
            var sut = new UserDtoMapper();

            var users = new List<User>()
            {
                  new User
                  {
                      Id = Guid.NewGuid(),
                      UserName = "testUsername@aaa.aa",
                      Email = "testUsername@aaa.aa"
                  },
                  new User
                  {
                      Id = Guid.NewGuid(),
                      UserName = "testUsername2@aaa.aa",
                      Email = "testUsername2@aaa.aa"
                  },
            };

            //Act
            var result = sut.MapFrom(users);

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<UserDto>));
        }

        [TestMethod]
        public void MapFromCollection_Should_ReturnCorrectCountUsersDto()
        {
            //Arrange
            var sut = new UserDtoMapper();

            var users = new List<User>()
            {
                  new User
                  {
                      Id = Guid.NewGuid(),
                      UserName = "testUsername@aaa.aa",
                      Email = "testUsername@aaa.aa"
                  },
                  new User
                  {
                      Id = Guid.NewGuid(),
                      UserName = "testUsername2@aaa.aa",
                      Email = "testUsername2@aaa.aa"
                  },
            };

            //Act
            var result = sut.MapFrom(users);

            //Assert
            Assert.AreEqual(2, result.Count());
        }
    }
}
