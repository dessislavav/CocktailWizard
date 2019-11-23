using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.BarRatingServiceTests
{
    [TestClass]
    public class CreateAsync_Should
    {
        [TestMethod]
        public async Task CorrectlyCreateBarRating()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(CorrectlyCreateBarRating));

            var mapperMock = new Mock<IDtoMapper<BarRating, BarRatingDto>>();

            var barId = Guid.NewGuid();
            var barIdTwo = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var userIdTwo = Guid.NewGuid();

            var createdOn = DateTime.UtcNow;

            var entityDto = new BarRatingDto
            {
                BarId = barId,
                UserId = userId,
                UserName = "testusername",
                Value = 2,
                CreatedOn = createdOn
            };

            var entity = new BarRating
            {
                BarId = barIdTwo,
                UserId = userIdTwo,
                Value = 2,
                CreatedOn = createdOn
            };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<BarRating>())).Returns(entityDto);

            using (var assertContext = new CWContext(options))
            {
                //Assert

                var sut = new BarRatingService(assertContext, mapperMock.Object);
                var result = await sut.CreateAsync(entityDto);

                Assert.IsInstanceOfType(result, typeof(BarRatingDto));
                Assert.AreEqual(barId, result.BarId);
                Assert.AreEqual(userId, result.UserId);
                Assert.AreEqual(userId, result.UserId);
                Assert.AreEqual(2, result.Value);
                Assert.AreEqual("testusername", result.UserName);

                Assert.AreEqual(entityDto.BarId, result.BarId);
                Assert.AreEqual(entityDto.UserId, result.UserId);
                Assert.AreEqual(entityDto.UserId, result.UserId);
                Assert.AreEqual(entityDto.Value, result.Value);
                Assert.AreEqual(entityDto.UserName, result.UserName);

            }
        }

        [TestMethod]
        public async Task ThrowWhen_DtoPassedIsNull()
        {

            //Arrange
            var options = TestUtilities.GetOptions(nameof(ThrowWhen_DtoPassedIsNull));

            var mapperMock = new Mock<IDtoMapper<BarRating, BarRatingDto>>();

            mapperMock.Setup(x => x.MapFrom(It.IsAny<BarRating>())).Returns(It.IsAny<BarRatingDto>);

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarRatingService(assertContext, mapperMock.Object);
                await Assert.ThrowsExceptionAsync<BusinessLogicException>(() => sut.CreateAsync(null));
            }
        }
    }
}
