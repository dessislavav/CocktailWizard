using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

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
            var mapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var searchMapperMock = new Mock<IDtoMapper<Bar, SearchBarDto>>();
            var cocktailMapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();

            using (var assertContext = new CWContext(options))
            {
                //Act 
                var sut = new BarService(assertContext, mapperMock.Object, searchMapperMock.Object, cocktailMapperMock.Object);

                //Assert
                Assert.IsNotNull(sut);
            }
        }

        [TestMethod]
        public void Throw_WhenContextIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_WhenContextIsNull));
            var mapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var searchMapperMock = new Mock<IDtoMapper<Bar, SearchBarDto>>();
            var cocktailMapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                Assert.ThrowsException<ArgumentNullException>(() => new BarService(null, mapperMock.Object, searchMapperMock.Object, cocktailMapperMock.Object));
            }
        }

        [TestMethod]
        public void Throw_WhenBarMapperIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_WhenBarMapperIsNull));
            var mapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var searchMapperMock = new Mock<IDtoMapper<Bar, SearchBarDto>>();
            var cocktailMapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                Assert.ThrowsException<ArgumentNullException>(() => new BarService(assertContext, null, searchMapperMock.Object, cocktailMapperMock.Object));
            }
        }

        [TestMethod]
        public void Throw_WhenSearchMapperIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_WhenSearchMapperIsNull));
            var mapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var searchMapperMock = new Mock<IDtoMapper<Bar, SearchBarDto>>();
            var cocktailMapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                Assert.ThrowsException<ArgumentNullException>(() => new BarService(assertContext, mapperMock.Object, null, cocktailMapperMock.Object));
            }
        }

        [TestMethod]
        public void Throw_WhenCocktailMapperIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_WhenSearchMapperIsNull));
            var mapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var searchMapperMock = new Mock<IDtoMapper<Bar, SearchBarDto>>();
            var cocktailMapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                Assert.ThrowsException<ArgumentNullException>(() => new BarService(assertContext, mapperMock.Object, searchMapperMock.Object, null));
            }
        }
    }
}
