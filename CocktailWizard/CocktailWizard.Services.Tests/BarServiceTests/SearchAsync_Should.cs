using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.BarServiceTests
{
    [TestClass]
    public class SearchAsync_Should
    {
        [TestMethod]
        public async Task ReturnCorrectEntitiesWhenSearchedBy_RatingOnly()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectEntitiesWhenSearchedBy_RatingOnly));
            var mapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var searchMapperMock = new Mock<IDtoMapper<Bar, SearchBarDto>>();
            var cocktailMapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var barId = Guid.NewGuid();
            var bar2Id = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var user2Id = Guid.NewGuid();

            var bars = new List<Bar> { new Bar
            {
                Id = barId,
                Name = "testBar",
                Address = "testAddress",
                ImagePath = "testImagePath",
            }, new Bar
            {
                Id = bar2Id,
                Name = "testBar2",
                Address = "testAddress2",
                ImagePath = "testImagePath2",
            }};

            var searchBars = new List<SearchBarDto> { new SearchBarDto
            {
                Id = barId,
                Name = "testBar",
                Address = "testAddress",
                ImagePath = "testImagePath",
            }, new SearchBarDto
            {
                Id = bar2Id,
                Name = "testBar2",
                Address = "testAddress2",
                ImagePath = "testImagePath2",
            }};


            var user = new User { Id = userId };
            var user2 = new User { Id = user2Id };

            var ratings = new List<BarRating> { new BarRating { BarId = barId, UserId = userId, Value = 4 }, new BarRating { BarId = barId, UserId = user2Id, Value = 5 } };

            searchMapperMock.Setup(x => x.MapFrom(It.IsAny<ICollection<Bar>>())).Returns(searchBars);

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                await assertContext.Bars.AddAsync(bars[0]);
                await assertContext.Bars.AddAsync(bars[1]);
                await assertContext.Users.AddAsync(user);
                await assertContext.Users.AddAsync(user2);
                await assertContext.BarRatings.AddAsync(ratings[0]);
                await assertContext.BarRatings.AddAsync(ratings[1]);
                await assertContext.SaveChangesAsync();
                var service = new BarService(assertContext, mapperMock.Object, searchMapperMock.Object, cocktailMapperMock.Object);
                var result = await service.SearchAsync(string.Empty, false, false, true, 3);

                Assert.AreEqual(2, result.Count);
                Assert.IsInstanceOfType(result, typeof(ICollection<SearchBarDto>));
            }
        }

        [TestMethod]
        public async Task ReturnCorrectEntitiesWhenSearchedBy_GeneralSearch()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectEntitiesWhenSearchedBy_GeneralSearch));
            var mapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var searchMapperMock = new Mock<IDtoMapper<Bar, SearchBarDto>>();
            var cocktailMapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var barId = Guid.NewGuid();
            var bar2Id = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var user2Id = Guid.NewGuid();

            var bars = new List<Bar> { new Bar
            {
                Id = barId,
                Name = "testBar",
                Address = "testAddress",
                ImagePath = "testImagePath",
            }, new Bar
            {
                Id = bar2Id,
                Name = "testBar2",
                Address = "testAddress2",
                ImagePath = "testImagePath2",
            }};

            var searchBars = new List<SearchBarDto> { new SearchBarDto
            {
                Id = barId,
                Name = "testBar",
                Address = "testAddress",
                ImagePath = "testImagePath",
            }, new SearchBarDto
            {
                Id = bar2Id,
                Name = "testBar2",
                Address = "testAddress2",
                ImagePath = "testImagePath2",
            }};


            var user = new User { Id = userId };
            var user2 = new User { Id = user2Id };

            var ratings = new List<BarRating> { new BarRating { BarId = barId, UserId = userId, Value = 4 }, new BarRating { BarId = barId, UserId = user2Id, Value = 5 } };

            searchMapperMock.Setup(x => x.MapFrom(It.IsAny<ICollection<Bar>>())).Returns(searchBars);

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                await assertContext.Bars.AddAsync(bars[0]);
                await assertContext.Bars.AddAsync(bars[1]);
                await assertContext.Users.AddAsync(user);
                await assertContext.Users.AddAsync(user2);
                await assertContext.BarRatings.AddAsync(ratings[0]);
                await assertContext.BarRatings.AddAsync(ratings[1]);
                await assertContext.SaveChangesAsync();
                var service = new BarService(assertContext, mapperMock.Object, searchMapperMock.Object, cocktailMapperMock.Object);
                var result = await service.SearchAsync("testBar2", false, false, false, 1);

                Assert.AreEqual(1, result.Count);
            }
        }

        [TestMethod]
        public async Task ReturnCorrectEntitiesWhenSearchedBy_CertainCriteria()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectEntitiesWhenSearchedBy_CertainCriteria));
            var mapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var searchMapperMock = new Mock<IDtoMapper<Bar, SearchBarDto>>();
            var cocktailMapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var barId = Guid.NewGuid();
            var bar2Id = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var user2Id = Guid.NewGuid();

            var bars = new List<Bar> { new Bar
            {
                Id = barId,
                Name = "testBar",
                Address = "testAddress",
                ImagePath = "testImagePath",
            }, new Bar
            {
                Id = bar2Id,
                Name = "testBar2",
                Address = "testAddress2",
                ImagePath = "testImagePath2",
            }};

            var searchBars = new List<SearchBarDto> { new SearchBarDto
            {
                Id = barId,
                Name = "testBar",
                Address = "testAddress",
                ImagePath = "testImagePath",
            }, new SearchBarDto
            {
                Id = bar2Id,
                Name = "testBar2",
                Address = "testAddress2",
                ImagePath = "testImagePath2",
            }};


            var user = new User { Id = userId };
            var user2 = new User { Id = user2Id };

            var ratings = new List<BarRating> { new BarRating { BarId = barId, UserId = userId, Value = 4 }, new BarRating { BarId = barId, UserId = user2Id, Value = 5 } };

            searchMapperMock.Setup(x => x.MapFrom(It.IsAny<ICollection<Bar>>())).Returns(searchBars);

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                await assertContext.Bars.AddAsync(bars[0]);
                await assertContext.Bars.AddAsync(bars[1]);
                await assertContext.Users.AddAsync(user);
                await assertContext.Users.AddAsync(user2);
                await assertContext.BarRatings.AddAsync(ratings[0]);
                await assertContext.BarRatings.AddAsync(ratings[1]);
                await assertContext.SaveChangesAsync();
                var service = new BarService(assertContext, mapperMock.Object, searchMapperMock.Object, cocktailMapperMock.Object);
                var result = await service.SearchAsync("testAddress2", false, true, false, 1);

                Assert.AreEqual(1, result.Count);
            }
        }
    }
}
