using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoMappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.BarServiceTests
{
    [TestClass]
    public class GetTopBarsAsync_Should
    {
        [TestMethod]
        public async Task ReturnCorrectInstanceOfCollection()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectInstanceOfCollection));
            var mapper = new BarDtoMapper();
            var searchMapper = new SearchBarDtoMapper();
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
                var sut = new BarService(assertContext, mapper, searchMapper);
                var result = await sut.GetTopBarsAsync(3);
                Assert.IsInstanceOfType(result, typeof(ICollection<BarDto>));
            }
        }

        [TestMethod]
        public async Task ReturnCorrectCountOfDtos()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectCountOfDtos));
            var mapper = new BarDtoMapper();
            var searchMapper = new SearchBarDtoMapper();
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
                var sut = new BarService(assertContext, mapper, searchMapper);
                var result = await sut.GetTopBarsAsync(2);
                Assert.AreEqual(2, result.Count());
            }
        }

        [TestMethod]
        public async Task ThrowWhen_NoBarsFound()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ThrowWhen_NoBarsFound));
            var mapper = new BarDtoMapper();
            var searchMapper = new SearchBarDtoMapper();

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarService(assertContext, mapper, searchMapper);
                await Assert.ThrowsExceptionAsync<BusinessLogicException>(() => sut.GetTopBarsAsync(1));
            }
        }
    }
}
