using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.DtoMappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.BarServiceTests
{
    [TestClass]
    public class GetPagesCountAsync_Should
    {
        [TestMethod]
        public async Task ReturnCorrectCountWhen_ValidValueIsPassed()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectCountWhen_ValidValueIsPassed));
            var mapper = new BarDtoMapper();
            var searchMapper = new SearchBarDtoMapper();
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

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Bars.AddAsync(bar1);
                await arrangeContext.Bars.AddAsync(bar2);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarService(assertContext, mapper, searchMapper);
                var result = await sut.GetPageCountAsync(10);
                Assert.AreEqual(1, result);
            }
        }

        [TestMethod]
        public async Task ReturnCorrectType_ValidValueIsPassed()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectCountWhen_ValidValueIsPassed));
            var mapper = new BarDtoMapper();
            var searchMapper = new SearchBarDtoMapper();
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

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Bars.AddAsync(bar1);
                await arrangeContext.Bars.AddAsync(bar2);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarService(assertContext, mapper, searchMapper);
                var result = await sut.GetPageCountAsync(10);
                Assert.IsInstanceOfType(result, typeof(int));
            }
        }
    }
}
