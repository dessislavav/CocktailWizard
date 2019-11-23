using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailWizard.Services.Tests.CocktailCommentServiceTests
{
    [TestClass]
    public class CocktailCommentService_Should
    {
        [TestMethod]
        public void CocktailCommentServiceConstructor_CreatesInstance()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(CocktailCommentServiceConstructor_CreatesInstance));

            var mapperMock = new Mock<IDtoMapper<CocktailComment, CocktailCommentDto>>();

            using (var assertContext = new CWContext(options))
            {
                //Act
                var sut = new CocktailCommentService(assertContext, mapperMock.Object);

                //Assert
                Assert.IsNotNull(sut);
            }
        }

        [TestMethod]
        public void Throw_WhenContextIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_WhenContextIsNull));

            var mapperMock = new Mock<IDtoMapper<CocktailComment, CocktailCommentDto>>();

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                Assert.ThrowsException<ArgumentNullException>(() => new CocktailCommentService(null, mapperMock.Object));
            }
        }

        [TestMethod]
        public void Throw_WhenCocktailCommentMapperIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_WhenCocktailCommentMapperIsNull));

            var mapperMock = new Mock<IDtoMapper<CocktailComment, CocktailCommentDto>>();

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                Assert.ThrowsException<ArgumentNullException>(() => new CocktailCommentService(assertContext, null));
            }
        }
    }
}
