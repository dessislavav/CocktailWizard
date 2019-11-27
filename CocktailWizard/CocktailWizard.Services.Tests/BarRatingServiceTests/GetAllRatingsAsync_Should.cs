using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.BarRatingServiceTests
{
    [TestClass]
    public class GetAllRatingsAsync_Should
    {
        [TestMethod]
        public async Task GetAllRatings_ReturnInstanceOfCollectionBarRatingDtos()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(GetAllRatings_ReturnInstanceOfCollectionBarRatingDtos));

            var mapperMock = new Mock<IDtoMapper<BarRating, BarRatingDto>>();

            var barId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var userIdTwo = Guid.NewGuid();

            var bar = new Bar
            {
                Id = barId,
                Name = "testName",

            };

            var user = new User
            {
                Id = userId,
                UserName = "testName"
            };

            var entity = new BarRating
            {
                BarId = barId,
                UserId = userId,
                Value = 2,
                CreatedOn = DateTime.UtcNow,
            };

            var entityTwo = new BarRating
            {
                BarId = barId,
                UserId = userIdTwo,
                Value = 2,
                CreatedOn = DateTime.UtcNow,
            };
             
            var list = new List<BarRatingDto>()
            {
                new BarRatingDto{ BarId = barId, UserId = userId, Value = 2 },
                new BarRatingDto { BarId = barId, UserId = userIdTwo, Value = 2},
            };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<ICollection<BarRating>>())).Returns(list);

            using (var arrangeContext = new CWContext(options))
            {
                //await arrangeContext.Bars.AddAsync(bar);
                //await arrangeContext.Users.AddAsync(user);
                await arrangeContext.BarRatings.AddAsync(entity);
                await arrangeContext.BarRatings.AddAsync(entityTwo);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarRatingService(assertContext, mapperMock.Object);

                var result = await sut.GetAllRatingsAsync(barId);

                Assert.IsInstanceOfType(result, typeof(ICollection<BarRatingDto>));
                Assert.AreEqual(2, result.Count());
                Assert.AreEqual(entity.Value, result.First().Value);
                Assert.AreEqual(entity.UserId, result.First().UserId);
                Assert.AreEqual(entityTwo.Value, result.Last().Value);
                Assert.AreEqual(entityTwo.UserId, result.Last().UserId);
            }

        }

    }
}
