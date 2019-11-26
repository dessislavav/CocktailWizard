using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.DtoMappers;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.BarServiceTests
{
    [TestClass]
    public class GetFiveBarsAsync_Should
    {
        [TestMethod]
        public async Task ReturnCorrectInstanceOfCollection()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectInstanceOfCollection));
            var mapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var searchMapperMock = new Mock<IDtoMapper<Bar, SearchBarDto>>();
            var cocktailMapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var testGuid = Guid.NewGuid();
            var testGuid2 = Guid.NewGuid();
            var testGuid3 = Guid.NewGuid();

            var bar1 = new Bar
            {
                Id = testGuid,
                Name = "testBar1",
            };
            var bar2 = new Bar
            {
                Id = testGuid2,
                Name = "testBar2",
            };
            var bar3 = new Bar
            {
                Id = testGuid3,
                Name = "testBar3",
            };

            var list = new List<BarDto>()
            {
                new BarDto{ Id = testGuid, Name = "testBar1" }, new BarDto { Id = testGuid2, Name = "testBar2"}, new BarDto { Id = testGuid3, Name = "testBar3"}
            };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<ICollection<Bar>>())).Returns(list);

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Bars.AddAsync(bar1);
                await arrangeContext.Bars.AddAsync(bar2);
                await arrangeContext.Bars.AddAsync(bar3);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarService(assertContext, mapperMock.Object, searchMapperMock.Object, cocktailMapperMock.Object);
                var result = await sut.GetFiveBarsAsync(1, null);
                Assert.IsInstanceOfType(result, typeof(ICollection<BarDto>));
                Assert.AreEqual(3, result.Count());
            }
        }

        [TestMethod]
        public async Task ReturnCorrectInstanceOfCollection_FromSecondPage()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectInstanceOfCollection_FromSecondPage));
            var mapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var searchMapperMock = new Mock<IDtoMapper<Bar, SearchBarDto>>();
            var cocktailMapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var testGuid = Guid.NewGuid();
            var testGuid2 = Guid.NewGuid();
            var testGuid3 = Guid.NewGuid();
            var testGuid4 = Guid.NewGuid();
            var testGuid5 = Guid.NewGuid();
            var testGuid6 = Guid.NewGuid();
            var testGuid7 = Guid.NewGuid();

            var bar1 = new Bar
            {
                Id = testGuid,
                Name = "testBar1",
            };
            var bar2 = new Bar
            {
                Id = testGuid2,
                Name = "testBar2",
            };
            var bar3 = new Bar
            {
                Id = testGuid3,
                Name = "testBar3",
            }; var bar4 = new Bar
            {
                Id = testGuid4,
                Name = "testBar4",
            }; var bar5 = new Bar
            {
                Id = testGuid5,
                Name = "testBar5",
            }; var bar6 = new Bar
            {
                Id = testGuid6,
                Name = "testBar6",
            }; var bar7 = new Bar
            {
                Id = testGuid7,
                Name = "testBar7",
            };

            var list = new List<BarDto>()
            {
                new BarDto{ Id = testGuid, Name = "testBar1" }, new BarDto { Id = testGuid2, Name = "testBar2"}
            };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<ICollection<Bar>>())).Returns(list);

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Bars.AddAsync(bar1);
                await arrangeContext.Bars.AddAsync(bar2);
                await arrangeContext.Bars.AddAsync(bar3);
                await arrangeContext.Bars.AddAsync(bar4);
                await arrangeContext.Bars.AddAsync(bar5);
                await arrangeContext.Bars.AddAsync(bar6);
                await arrangeContext.Bars.AddAsync(bar7);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarService(assertContext, mapperMock.Object, searchMapperMock.Object, cocktailMapperMock.Object);
                var result = await sut.GetFiveBarsAsync(2, null);
                Assert.IsInstanceOfType(result, typeof(ICollection<BarDto>));
                Assert.AreEqual(2, result.Count());
            }
        }

        [TestMethod]
        public async Task ReturnCorrectElementsWhen_SortIsByName()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectElementsWhen_SortIsByName));
            var mapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var searchMapperMock = new Mock<IDtoMapper<Bar, SearchBarDto>>();
            var cocktailMapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var testGuid = Guid.NewGuid();
            var testGuid2 = Guid.NewGuid();

            var bar1 = new Bar
            {
                Id = testGuid,
                Name = "atestBar",
            };
            var bar2 = new Bar
            {
                Id = testGuid2,
                Name = "ztestBar",
            };

            var list = new List<BarDto>()
            {
                new BarDto{ Id = testGuid, Name = "atestBar" }, new BarDto { Id = testGuid2, Name = "ztestBar"}
            };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<ICollection<Bar>>())).Returns(list);

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Bars.AddAsync(bar1);
                await arrangeContext.Bars.AddAsync(bar2);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarService(assertContext, mapperMock.Object, searchMapperMock.Object, cocktailMapperMock.Object);
                var result = await sut.GetFiveBarsAsync(1, "name_desc");
                Assert.AreEqual("atestBar", result.ToArray()[0].Name);
                Assert.AreEqual(testGuid, result.ToArray()[0].Id);
                Assert.AreEqual("ztestBar", result.ToArray()[1].Name);
                Assert.AreEqual(testGuid2, result.ToArray()[1].Id);
            }
        }

        [TestMethod]
        public async Task ReturnCorrectElementsWhen_SortIsByNameAsc()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectElementsWhen_SortIsByNameAsc));
            var mapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var searchMapperMock = new Mock<IDtoMapper<Bar, SearchBarDto>>();
            var cocktailMapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var testGuid = Guid.NewGuid();
            var testGuid2 = Guid.NewGuid();

            var bar1 = new Bar
            {
                Id = testGuid,
                Name = "atestBar",
            };
            var bar2 = new Bar
            {
                Id = testGuid2,
                Name = "ztestBar",
            };

            var list = new List<BarDto>()
            {
                new BarDto{ Id = testGuid2, Name = "ztestBar" }, new BarDto { Id = testGuid, Name = "atestBar"}
            };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<ICollection<Bar>>())).Returns(list);

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Bars.AddAsync(bar1);
                await arrangeContext.Bars.AddAsync(bar2);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarService(assertContext, mapperMock.Object, searchMapperMock.Object, cocktailMapperMock.Object);
                var result = await sut.GetFiveBarsAsync(1, "Name");
                Assert.AreEqual("atestBar", result.ToArray()[1].Name);
                Assert.AreEqual(testGuid, result.ToArray()[1].Id);
                Assert.AreEqual("ztestBar", result.ToArray()[0].Name);
                Assert.AreEqual(testGuid2, result.ToArray()[0].Id);
            }
        }

        [TestMethod]
        public async Task ReturnCorrectElementsWhen_SortIsByRating()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectElementsWhen_SortIsByRating));
            var mapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var searchMapperMock = new Mock<IDtoMapper<Bar, SearchBarDto>>();
            var cocktailMapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var testGuid = Guid.NewGuid();
            var testGuid2 = Guid.NewGuid();
            var testGuid3 = Guid.NewGuid();

            var bar1 = new Bar
            {
                Id = testGuid,
                Name = "atestBar",
            };
            var bar2 = new Bar
            {
                Id = testGuid2,
                Name = "ztestBar",
            };

            var user = new User { Id = testGuid3, UserName = "testUser" };

            var barRating = new BarRating { BarId = bar1.Id, UserId = user.Id, Value = 5 };

            var list = new List<BarDto>()
            {
                new BarDto{ Id = testGuid, Name = "atestBar" }, new BarDto { Id = testGuid2, Name = "ztestBar"}
            };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<ICollection<Bar>>())).Returns(list);

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Bars.AddAsync(bar1);
                await arrangeContext.Bars.AddAsync(bar2);
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.BarRatings.AddAsync(barRating);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarService(assertContext, mapperMock.Object, searchMapperMock.Object, cocktailMapperMock.Object);
                var result = await sut.GetFiveBarsAsync(1, "rating_desc");
                Assert.AreEqual("atestBar", result.ToArray()[0].Name);
                Assert.AreEqual(testGuid, result.ToArray()[0].Id);
                Assert.AreEqual("ztestBar", result.ToArray()[1].Name);
                Assert.AreEqual(testGuid2, result.ToArray()[1].Id);
            }
        }

        [TestMethod]
        public async Task ReturnCorrectElementsWhen_SortIsByRatingAsc()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectElementsWhen_SortIsByRatingAsc));
            var mapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var searchMapperMock = new Mock<IDtoMapper<Bar, SearchBarDto>>();
            var cocktailMapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var testGuid = Guid.NewGuid();
            var testGuid2 = Guid.NewGuid();
            var testGuid3 = Guid.NewGuid();

            var bar1 = new Bar
            {
                Id = testGuid,
                Name = "atestBar",
            };
            var bar2 = new Bar
            {
                Id = testGuid2,
                Name = "ztestBar",
            };

            var user = new User { Id = testGuid3, UserName = "testUser" };

            var barRating = new BarRating { BarId = bar1.Id, Bar = bar1, User = user, UserId = user.Id, Value = 5 };

            user.BarRatings.Add(barRating);
            bar1.Ratings.Add(barRating);

            var list = new List<BarDto>()
            {
                new BarDto{ Id = testGuid2, Name = "ztestBar" }, new BarDto { Id = testGuid, Name = "atestBar"}
            };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<ICollection<Bar>>())).Returns(list);

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Bars.AddAsync(bar1);
                await arrangeContext.Bars.AddAsync(bar2);
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.BarRatings.AddAsync(barRating);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarService(assertContext, mapperMock.Object, searchMapperMock.Object, cocktailMapperMock.Object);
                var result = await sut.GetFiveBarsAsync(1, "Rating");
                Assert.AreEqual("ztestBar", result.ToArray()[0].Name);
                Assert.AreEqual(testGuid2, result.ToArray()[0].Id);
                Assert.AreEqual("atestBar", result.ToArray()[1].Name);
                Assert.AreEqual(testGuid, result.ToArray()[1].Id);
            }
        }
    }
}


//public async Task<ICollection<BarDto>> GetFiveBarsAsync(int currentPage, string sortOrder)
//{
//    try
//    {
//        IQueryable<Bar> bars = this.context.Bars
//            .Include(b => b.Ratings)
//            .Where(b => b.IsDeleted == false);

//        ICollection<Bar> fiveBars;

//        switch (sortOrder)
//        {
//            case "Name":
//                bars = bars.OrderBy(b => b.Name);
//                break;
//            case "name_desc":
//                bars = bars.OrderByDescending(b => b.Name);
//                break;
//            case "Rating":
//                bars = bars.OrderBy(b => b.Ratings.Count());
//                break;
//            case "rating_desc":
//                bars = bars.OrderByDescending(b => b.Ratings.Count());
//                break;
//            default:
//                bars = bars.OrderBy(b => b.Name);
//                break;
//        }

//        if (currentPage == 1)
//        {
//            fiveBars = await bars
//                .Take(5)
//                .ToListAsync();
//        }
//        else
//        {
//            fiveBars = await bars
//                .Skip((currentPage - 1) * 5)
//                .Take(5)
//                .ToListAsync();
//        }

//        var dtoBars = this.dtoMapper.MapFrom(fiveBars);

//        return dtoBars;
//    }
//    catch (Exception)
//    {
//        throw new BusinessLogicException(ExceptionMessages.BarNull);
//    }
//}