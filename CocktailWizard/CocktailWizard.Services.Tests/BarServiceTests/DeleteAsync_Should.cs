using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoMappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.BarServiceTests
{
    [TestClass]
    public class DeleteAsync_Should
    {
        [TestMethod]
        public async Task CorrectlyDeleteBar()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(CorrectlyDeleteBar));
            var mapper = new BarDtoMapper();
            var searchMapper = new SearchBarDtoMapper();
            var guid = Guid.NewGuid();

            var entity = new Bar
            {
                Id = guid,
                Name = "testBar",
                Info = "testInfo",
                Address = "testAddress",
                ImagePath = "testImagePath",
                Phone = "111-333-666"
            };

            using (var actContext = new CWContext(options))
            {
                //Act
                await actContext.Bars.AddAsync(entity);
                await actContext.SaveChangesAsync();
                var service = new BarService(actContext, mapper, searchMapper);
                var result = await service.DeleteAsync(guid);
                await actContext.SaveChangesAsync();
            }
            using (var assertContext = new CWContext(options))
            {
                //Assert
                var result = await assertContext.Bars.FirstAsync();
                Assert.AreEqual(true, result.IsDeleted);
            }
        }

        [TestMethod]
        public async Task ThrowWhen_NoBarFound()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ThrowWhen_NoBarFound));
            var mapper = new BarDtoMapper();
            var searchMapper = new SearchBarDtoMapper();
            var guid = Guid.NewGuid();

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarService(assertContext, mapper, searchMapper);
                await Assert.ThrowsExceptionAsync<BusinessLogicException>(() => sut.DeleteAsync(guid));
            }
        }

        [TestMethod]
        public async Task ReturnCorrectTypeOfInstance()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectTypeOfInstance));
            var mapper = new BarDtoMapper();
            var searchMapper = new SearchBarDtoMapper();
            var guid = Guid.NewGuid();

            var entity = new Bar
            {
                Id = guid,
                Name = "testBar",
                Info = "testInfo",
                Address = "testAddress",
                ImagePath = "testImagePath",
                Phone = "111-333-666"
            };

            using (var actContext = new CWContext(options))
            {
                //Act
                await actContext.Bars.AddAsync(entity);
                await actContext.SaveChangesAsync();
            }
            using (var assertContext = new CWContext(options))
            {
                //Assert
                var sut = new BarService(assertContext, mapper, searchMapper);
                var result = await sut.DeleteAsync(guid);
                Assert.IsInstanceOfType(result, typeof(BarDto));
            }
        }
    }
}
