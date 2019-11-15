using CocktailWizard.Data.AppContext;
using CocktailWizard.Services.DtoMappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailWizard.Services.Tests.BarServiceTests
{
    [TestClass]
    public class BarServiceConstructor_Should
    {
        [TestMethod]
        public void BarServiceConstructor_CreatesInstance()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(BarServiceConstructor_CreatesInstance));
            var mapper = new BarDtoMapper();
            var searchMapper = new SearchBarDtoMapper();

            using (var assertContext = new CWContext(options))
            {
                //Act 
                var sut = new BarService(assertContext, mapper, searchMapper);

                //Assert
                Assert.IsNotNull(sut);
            }
        }

        [TestMethod]
        public void Throw_WhenContextIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_WhenContextIsNull));
            var mapper = new BarDtoMapper();
            var searchMapper = new SearchBarDtoMapper();

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                Assert.ThrowsException<ArgumentNullException>(() => new BarService(null, mapper, searchMapper));
            }
        }

        [TestMethod]
        public void Throw_WhenBarMapperIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_WhenBarMapperIsNull));
            var mapper = new BarDtoMapper();
            var searchMapper = new SearchBarDtoMapper();

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                Assert.ThrowsException<ArgumentNullException>(() => new BarService(assertContext, null, searchMapper));
            }
        }

        [TestMethod]
        public void Throw_WhenSearchMapperIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_WhenSearchMapperIsNull));
            var mapper = new BarDtoMapper();
            var searchMapper = new SearchBarDtoMapper();

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                Assert.ThrowsException<ArgumentNullException>(() => new BarService(assertContext, mapper, null));
            }
        }
    }
}
