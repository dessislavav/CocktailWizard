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
    public class GetTenBarsOrderedByNameAsync_Should
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
                var result = await sut.GetTenBarsOrderedByNameAsync(1);
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
            var testGuid8 = Guid.NewGuid();
            var testGuid9 = Guid.NewGuid();
            var testGuid10 = Guid.NewGuid();
            var testGuid11 = Guid.NewGuid();
            var testGuid12 = Guid.NewGuid();

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
            }; var bar8 = new Bar
            {
                Id = testGuid8,
                Name = "testBar8",
            }; var bar9 = new Bar
            {
                Id = testGuid9,
                Name = "testBar9",
            }; var bar10 = new Bar
            {
                Id = testGuid10,
                Name = "testBar10",
            }; var bar11 = new Bar
            {
                Id = testGuid11,
                Name = "testBar11",
            }; var bar12 = new Bar
            {
                Id = testGuid12,
                Name = "testBar12",
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
                await arrangeContext.Bars.AddAsync(bar8);
                await arrangeContext.Bars.AddAsync(bar9);
                await arrangeContext.Bars.AddAsync(bar10);
                await arrangeContext.Bars.AddAsync(bar11);
                await arrangeContext.Bars.AddAsync(bar12);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarService(assertContext, mapperMock.Object, searchMapperMock.Object, cocktailMapperMock.Object);
                var result = await sut.GetTenBarsOrderedByNameAsync(2);
                Assert.IsInstanceOfType(result, typeof(ICollection<BarDto>));
                Assert.AreEqual(2, result.Count());
            }
        }
    }
}
