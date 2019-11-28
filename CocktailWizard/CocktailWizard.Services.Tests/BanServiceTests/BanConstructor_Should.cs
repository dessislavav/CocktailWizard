using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace CocktailWizard.Services.Tests.BanServiceTests
{
    [TestClass]
    public class BanConstructor_Should
    {
        [TestMethod]
        public void Constructor_CreatesInstance()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Constructor_CreatesInstance));
            var mapperMock = new Mock<IDtoMapper<User, UserDto>>();

            using (var assertContext = new CWContext(options))
            {
                //Act 
                var sut = new BanService(assertContext, mapperMock.Object);

                //Assert
                Assert.IsNotNull(sut);
            }
        }

        [TestMethod]
        public void Throw_WhenContextIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_WhenContextIsNull));
            var mapperMock = new Mock<IDtoMapper<User, UserDto>>();

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                Assert.ThrowsException<ArgumentNullException>(() => new BanService(null, mapperMock.Object));
            }
        }

        [TestMethod]
        public void Throw_WhenMapperIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_WhenMapperIsNull));

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                Assert.ThrowsException<ArgumentNullException>(() => new BanService(assertContext, null));
            }
        }
    }
}
