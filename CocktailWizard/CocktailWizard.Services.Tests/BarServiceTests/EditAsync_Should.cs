using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.BarServiceTests
{
    [TestClass]
    public class EditAsync_Should
    {
        [TestMethod]
        public async Task CorrectlyUpdateEntity()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(CorrectlyUpdateEntity));
            var mapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var searchMapperMock = new Mock<IDtoMapper<Bar, SearchBarDto>>();
            var cocktailMapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var testGuid = Guid.NewGuid();

            var bar = new Bar
            {
                Id = testGuid,
                Name = "testBar",
                Info = "testInfo",
                Address = "testAddress",
                Phone = "111-333-666",
            };

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Bars.AddAsync(bar);
                await arrangeContext.SaveChangesAsync();
            }

            mapperMock.Setup(x => x.MapFrom(It.IsAny<Bar>())).Returns(It.IsAny<BarDto>);

            using (var assertContext = new CWContext(options))
            {
                var sut = new BarService(assertContext, mapperMock.Object, searchMapperMock.Object, cocktailMapperMock.Object);
                var result = await sut.EditAsync(testGuid, "newTestName", "newTestInfo", "newTestAddress", "111-333-6667");
                var edittedBar = await assertContext.Bars.FirstAsync();
                Assert.AreEqual("newTestName", edittedBar.Name);
                Assert.AreEqual("newTestInfo", edittedBar.Info);
                Assert.AreEqual("newTestAddress", edittedBar.Address);
                Assert.AreEqual("111-333-6667", edittedBar.Phone);
            }
        }

        [TestMethod]
        public async Task ReturnCorrectTypeOfEntity()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectTypeOfEntity));
            var mapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var searchMapperMock = new Mock<IDtoMapper<Bar, SearchBarDto>>();
            var cocktailMapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var testGuid = Guid.NewGuid();

            var bar = new Bar
            {
                Id = testGuid,
                Name = "testBar",
                Info = "testInfo",
                Address = "testAddress",
                Phone = "111-333-666",
            };

            var dtoEntity = new BarDto
            {
                Id = testGuid,
                Name = "testBar",
                Info = "testInfo",
                Address = "testAddress",
                Phone = "111-333-666"
            };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<Bar>())).Returns(dtoEntity);

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Bars.AddAsync(bar);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                var sut = new BarService(assertContext, mapperMock.Object, searchMapperMock.Object, cocktailMapperMock.Object);
                var result = await sut.EditAsync(testGuid, "newTestName", "newTestInfo", "newTestAddress", "111-333-6667");
                Assert.IsInstanceOfType(result, typeof(BarDto));
            }
        }

        [TestMethod]
        public async Task ThrowWhen_NoBarFound()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ThrowWhen_NoBarFound));
            var mapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var searchMapperMock = new Mock<IDtoMapper<Bar, SearchBarDto>>();
            var cocktailMapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var testGuid = Guid.NewGuid();
            var testGuid2 = Guid.NewGuid();

            var bar = new Bar
            {
                Id = testGuid,
                Name = "testBar",
                Info = "testInfo",
                Address = "testAddress",
                Phone = "111-333-666",
            };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<Bar>())).Returns(It.IsAny<BarDto>);

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Bars.AddAsync(bar);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                var sut = new BarService(assertContext, mapperMock.Object, searchMapperMock.Object, cocktailMapperMock.Object);
                await Assert.ThrowsExceptionAsync<BusinessLogicException>(() => sut.EditAsync(testGuid2, "newTestName", "newTestInfo", "newTestAddress", "111-333-6667"));
            }
        }
    }
}
