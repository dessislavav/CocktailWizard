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

namespace CocktailWizard.Services.Tests.CocktailRatingServiceTests
{
    [TestClass]
    public class GetAllRatingsAsync_Should
    {
        [TestMethod]
        public async Task ReturnInstanceOfCollectionCocktailRatingDtos()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnInstanceOfCollectionCocktailRatingDtos));

            var mapperMock = new Mock<IDtoMapper<CocktailRating, CocktailRatingDto>>();

            var cocktailId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var userIdTwo = Guid.NewGuid();

            var cocktail = new Cocktail
            {
                Id = cocktailId,
                Name = "testName"
            };

            var user = new User
            {
                Id = userId,
                UserName = "testName"
            };

            var entity = new CocktailRating
            {
                CocktailId = cocktailId,
                UserId = userId,
                Value = 2,
            };

            var entityTwo = new CocktailRating
            {
                CocktailId = cocktailId,
                UserId = userIdTwo,
                Value = 2,
            };

            var list = new List<CocktailRatingDto>()
            {
                new CocktailRatingDto{ CocktailId = cocktailId, UserId = userId, Value = 2 },
                new CocktailRatingDto { CocktailId = cocktailId, UserId = userIdTwo, Value = 2},
            };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<ICollection<CocktailRating>>())).Returns(list);

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Cocktails.AddAsync(cocktail);
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.CocktailRatings.AddAsync(entity);
                await arrangeContext.CocktailRatings.AddAsync(entityTwo);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new CocktailRatingService(assertContext, mapperMock.Object);

                var result = await sut.GetAllRatingsAsync(cocktailId);

                Assert.IsInstanceOfType(result, typeof(ICollection<CocktailRatingDto>));
                Assert.AreEqual(2, result.Count());
                Assert.AreEqual(entity.Value, result.First().Value);
                Assert.AreEqual(entity.UserId, result.First().UserId);
                Assert.AreEqual(entityTwo.Value, result.Last().Value);
                Assert.AreEqual(entityTwo.UserId, result.Last().UserId);
            }
        }

       
    }
}
