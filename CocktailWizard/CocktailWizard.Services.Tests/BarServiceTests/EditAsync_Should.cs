using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoMappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var mapper = new BarDtoMapper();
            var searchMapper = new SearchBarDtoMapper();
            var testGuid = Guid.NewGuid();

            var bar = new Bar
            {
                Id = testGuid,
                Name = "testBar",
                Info = "testInfo",
                Address = "testAddress",
                Phone = "111-333-666",
                ImagePath = "testImagePath",
            };

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Bars.AddAsync(bar);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                var sut = new BarService(assertContext, mapper, searchMapper);
                var result = await sut.EditAsync(testGuid, "newTestName", "newTestInfo", "newTestAddress", "111-333-6667", "newImagePath");
                var edittedBar = await assertContext.Bars.FirstAsync();
                Assert.AreEqual("newTestName", edittedBar.Name);
                Assert.AreEqual("newTestInfo", edittedBar.Info);
                Assert.AreEqual("newTestAddress", edittedBar.Address);
                Assert.AreEqual("111-333-6667", edittedBar.Phone);
                Assert.AreEqual("newImagePath", edittedBar.ImagePath);
            }
        }

        [TestMethod]
        public async Task ReturnCorrectTypeOfEntity()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectTypeOfEntity));
            var mapper = new BarDtoMapper();
            var searchMapper = new SearchBarDtoMapper();
            var testGuid = Guid.NewGuid();

            var bar = new Bar
            {
                Id = testGuid,
                Name = "testBar",
                Info = "testInfo",
                Address = "testAddress",
                Phone = "111-333-666",
                ImagePath = "testImagePath",
            };

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Bars.AddAsync(bar);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                var sut = new BarService(assertContext, mapper, searchMapper);
                var result = await sut.EditAsync(testGuid, "newTestName", "newTestInfo", "newTestAddress", "111-333-6667", "newImagePath");
                Assert.IsInstanceOfType(result, typeof(BarDto));
            }
        }

        [TestMethod]
        public async Task ThrowWhen_NoBarFound()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ThrowWhen_NoBarFound));
            var mapper = new BarDtoMapper();
            var searchMapper = new SearchBarDtoMapper();
            var testGuid = Guid.NewGuid();
            var testGuid2 = Guid.NewGuid();

            var bar = new Bar
            {
                Id = testGuid,
                Name = "testBar",
                Info = "testInfo",
                Address = "testAddress",
                Phone = "111-333-666",
                ImagePath = "testImagePath",
            };

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Bars.AddAsync(bar);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                var sut = new BarService(assertContext, mapper, searchMapper);
                await Assert.ThrowsExceptionAsync<BusinessLogicException>(() => sut.EditAsync(testGuid2, "newTestName", "newTestInfo", "newTestAddress", "111-333-6667", "newImagePath"));
            }
        }
    }
}
