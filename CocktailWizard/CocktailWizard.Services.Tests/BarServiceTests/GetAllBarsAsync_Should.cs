using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.BarServiceTests
{
    [TestClass]
    public class GetAllBarsAsync_Should
    {
        [TestMethod]
        public async Task ReturnInstanceOfCollectionTypeBarDto()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnInstanceOfCollectionTypeBarDto));
            var mapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var searchMapperMock = new Mock<IDtoMapper<Bar, SearchBarDto>>();
            var cocktailMapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var testGuid = Guid.NewGuid();
            var testGuid2 = Guid.NewGuid();

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

            var list = new List<BarDto>()
            {
                new BarDto{ Id = testGuid, Name = "testBar1" }, new BarDto { Id = testGuid2, Name = "testBar2"}
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
                var result = await sut.GetAllBarsAsync();
                Assert.IsInstanceOfType(result, typeof(ICollection<BarDto>));
                Assert.AreEqual(2, result.Count());
                Assert.AreEqual(bar1.Name, result.First().Name);
                Assert.AreEqual(bar1.Id, result.First().Id);
                Assert.AreEqual(bar2.Name, result.Last().Name);
                Assert.AreEqual(bar2.Id, result.Last().Id);
            }
        }

        [TestMethod]
        public async Task ThrowWhen_NoBarsFound()
        {
            var options = TestUtilities.GetOptions(nameof(ThrowWhen_NoBarsFound));
            var mapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var searchMapperMock = new Mock<IDtoMapper<Bar, SearchBarDto>>();
            var cocktailMapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarService(assertContext, mapperMock.Object, searchMapperMock.Object, cocktailMapperMock.Object);
                await Assert.ThrowsExceptionAsync<BusinessLogicException>(() => sut.GetAllBarsAsync());
            }
        }
    }
}
