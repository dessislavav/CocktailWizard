using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.CocktailRatingServiceTests
{
    [TestClass]
    public class CreateAsync_Should
    {
        [TestMethod]
        public async Task CorrectlyCreateCocktailRating()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(CorrectlyCreateCocktailRating));

            var mapperMock = new Mock<IDtoMapper<CocktailRating, CocktailRatingDto>>();

            var cocktailId = Guid.NewGuid();
            var cocktailIdTwo = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var userIdTwo = Guid.NewGuid();

            var createdOn = DateTime.UtcNow;

            var entityDto = new CocktailRatingDto
            {
                CocktailId = cocktailId,
                UserId = userId,
                UserName = "testusername",
                Value = 2,
                CreatedOn = createdOn
            };

            var entity = new CocktailRating
            {
                CocktailId = cocktailId,
                UserId = userIdTwo,
                Value = 2,
                CreatedOn = createdOn
            };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<CocktailRating>())).Returns(entityDto);

            using (var assertContext = new CWContext(options))
            {
                //Assert

                var sut = new CocktailRatingService(assertContext, mapperMock.Object);
                var result = await sut.CreateAsync(entityDto);

                Assert.IsInstanceOfType(result, typeof(CocktailRatingDto));
                Assert.AreEqual(cocktailId, result.CocktailId);
                Assert.AreEqual(userId, result.UserId);
                Assert.AreEqual(userId, result.UserId);
                Assert.AreEqual(2, result.Value);
                Assert.AreEqual("testusername", result.UserName);

                Assert.AreEqual(entityDto.CocktailId, result.CocktailId);
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

            var mapperMock = new Mock<IDtoMapper<CocktailRating, CocktailRatingDto>>();

            mapperMock.Setup(x => x.MapFrom(It.IsAny<CocktailRating>())).Returns(It.IsAny<CocktailRatingDto>);

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new CocktailRatingService(assertContext, mapperMock.Object);
                await Assert.ThrowsExceptionAsync<BusinessLogicException>(() => sut.CreateAsync(null));
            }
        }
    }
}
