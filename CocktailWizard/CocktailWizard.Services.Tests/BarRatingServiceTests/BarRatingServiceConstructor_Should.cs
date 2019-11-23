using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailWizard.Services.Tests.BarRatingServiceTests
{
    [TestClass]
    public class BarRatingServiceConstructor_Should
    {
        [TestMethod]
        public void BarRatingServiceConstructor_CreatesInstance()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(BarRatingServiceConstructor_CreatesInstance));

            var mapperMock = new Mock<IDtoMapper<BarRating, BarRatingDto>>();

            using (var assertContext = new CWContext(options))
            {
                //Act
                var sut = new BarRatingService(assertContext, mapperMock.Object);

                //Assert
                Assert.IsNotNull(sut);
            }
        }

        [TestMethod]
        public void Throw_WhenContextIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_WhenContextIsNull));

            var mapperMock = new Mock<IDtoMapper<BarRating, BarRatingDto>>();

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                Assert.ThrowsException<ArgumentNullException>(() => new BarRatingService(null, mapperMock.Object));
            }
        }

        [TestMethod]
        public void Throw_WhenBarRatingMapperIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_WhenBarRatingMapperIsNull));

            var mapperMock = new Mock<IDtoMapper<BarRating, BarRatingDto>>();

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                Assert.ThrowsException<ArgumentNullException>(() => new BarRatingService(assertContext, null));
            }
        }
    }
}
