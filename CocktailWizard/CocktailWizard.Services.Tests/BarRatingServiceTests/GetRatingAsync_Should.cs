using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.BarRatingServiceTests
{
    [TestClass]
    public class GetRatingAsync_Should
    {
        [TestMethod]
        public async Task ReturnCorrectInstanceOfBarRatingDto()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectInstanceOfBarRatingDto));

            var mapperMock = new Mock<IDtoMapper<BarRating, BarRatingDto>>();

            var barId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var userIdTwo = Guid.NewGuid();

            var bar = new Bar
            {
                Name = "BarTest",
                Id = barId,
            };
            var user = new User
            {
                UserName = "userTest",
                Id = userId,
            };

            var entity = new BarRating
            {
                BarId = bar.Id,
                UserId = user.Id,
                Value = 2
            };

            var entityDto = new BarRatingDto
            {
                BarId = bar.Id,
                UserId = user.Id,
                Value = 2
            };



            mapperMock.Setup(x => x.MapFrom(It.IsAny<BarRating>())).Returns(entityDto);

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Bars.AddAsync(bar);
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.BarRatings.AddAsync(entity);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarRatingService(assertContext, mapperMock.Object);

                var result = await sut.GetRatingAsync(barId, userId);

                Assert.IsInstanceOfType(result, typeof(BarRatingDto));
                Assert.AreEqual(entity.Value, result.Value);
                Assert.AreEqual(entity.UserId, result.UserId);

            }
        }

        [TestMethod]
        public async Task Throw_WhenNoBarRatingDtoFound()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_WhenNoBarRatingDtoFound));

            var mapperMock = new Mock<IDtoMapper<BarRating, BarRatingDto>>();

            var barId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var userIdTwo = Guid.NewGuid();

            var entity = new BarRating
            {
                BarId = barId,
                UserId = userId,
                Value = 2
            };

            var entityDto = new BarRatingDto
            {
                BarId = barId,
                UserId = userId,
                Value = 2
            };



            mapperMock.Setup(x => x.MapFrom(It.IsAny<BarRating>())).Returns(entityDto);

            using (var arrangeContext = new CWContext(options))
            {

                await arrangeContext.BarRatings.AddAsync(entity);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarRatingService(assertContext, mapperMock.Object);

                await Assert.ThrowsExceptionAsync<BusinessLogicException>(() => sut.GetRatingAsync(barId, userId));

            }
        }
    }
}
