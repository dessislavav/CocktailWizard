using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace CocktailWizard.Services.Tests.BarCommentServiceTests
{
    [TestClass]
    public class BarCommentServiceConstructor_Should
    {
        [TestMethod]
        public void BarCommentServiceConstructor_CreatesInstance()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(BarCommentServiceConstructor_CreatesInstance));

            var mapperMock = new Mock<IDtoMapper<BarComment, BarCommentDto>>();

            using (var assertContext = new CWContext(options))
            {
                //Act
                var sut = new BarCommentService(assertContext, mapperMock.Object);

                //Assert
                Assert.IsNotNull(sut);
            }
        }

        [TestMethod]
        public void Throw_WhenContextIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_WhenContextIsNull));

            var mapperMock = new Mock<IDtoMapper<BarComment, BarCommentDto>>();

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                Assert.ThrowsException<ArgumentNullException>(() => new BarCommentService(null, mapperMock.Object));
            }
        }

        [TestMethod]
        public void Throw_WhenBarCommentMapperIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_WhenBarCommentMapperIsNull));

            var mapperMock = new Mock<IDtoMapper<BarComment, BarCommentDto>>();

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                Assert.ThrowsException<ArgumentNullException>(() => new BarCommentService(assertContext, null));
            }
        }
    }
}
