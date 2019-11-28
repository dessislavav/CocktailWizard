using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace CocktailWizard.Services.Tests.CocktailRatingServiceTests
{
    [TestClass]
    public class CocktailRatingServiceConstructor_Should
    {
        [TestMethod]
        public void CocktailRatingServiceConstructor_CreatesInstance()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(CocktailRatingServiceConstructor_CreatesInstance));

            var mapperMock = new Mock<IDtoMapper<CocktailRating, CocktailRatingDto>>();

            using (var assertContext = new CWContext(options))
            {
                //Act
                var sut = new CocktailRatingService(assertContext, mapperMock.Object);

                //Assert
                Assert.IsNotNull(sut);
            }
        }

        [TestMethod]
        public void Throw_WhenContextIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_WhenContextIsNull));

            var mapperMock = new Mock<IDtoMapper<CocktailRating, CocktailRatingDto>>();

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                Assert.ThrowsException<ArgumentNullException>(() => new CocktailRatingService(null, mapperMock.Object));
            }
        }

        [TestMethod]
        public void Throw_WhenCocktailRatingMapperIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_WhenCocktailRatingMapperIsNull));

            var mapperMock = new Mock<IDtoMapper<CocktailRating, CocktailRatingDto>>();

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                Assert.ThrowsException<ArgumentNullException>(() => new CocktailRatingService(assertContext, null));
            }
        }
    }
}
