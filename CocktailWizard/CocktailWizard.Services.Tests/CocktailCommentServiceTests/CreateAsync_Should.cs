using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.CocktailCommentServiceTests
{
    [TestClass]
    public class CreateAsync_Should
    {
        [TestMethod]
        public async Task CorrectlyCreateCocktailComment()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(CorrectlyCreateCocktailComment));

            var mapperMock = new Mock<IDtoMapper<CocktailComment, CocktailCommentDto>>();

            var id = Guid.NewGuid();
            var cocktailId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var createdOn = DateTime.UtcNow;

            var entityDto = new CocktailCommentDto
            {
                Id = id,
                CocktailId = cocktailId,
                UserId = userId,
                UserName = "testusername",
                Body = "testbody",
                CreatedOn = createdOn
            };

            var entity = new CocktailComment
            {
                Id = id,
                CocktailId = cocktailId,
                UserId = userId,
                Body = "testbody",
                CreatedOn = createdOn
            };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<CocktailComment>())).Returns(entityDto);

            using (var assertContext = new CWContext(options))
            {
                //Assert

                var sut = new CocktailCommentService(assertContext, mapperMock.Object);
                var result = await sut.CreateAsync(entityDto);

                Assert.IsInstanceOfType(result, typeof(CocktailCommentDto));
                Assert.AreEqual(id, result.Id);
                Assert.AreEqual(cocktailId, result.CocktailId);
                Assert.AreEqual(userId, result.UserId);
                Assert.AreEqual("testbody", result.Body);
                Assert.AreEqual("testusername", result.UserName);

                Assert.AreEqual(entityDto.Id, result.Id);
                Assert.AreEqual(entityDto.CocktailId, result.CocktailId);
                Assert.AreEqual(entityDto.UserId, result.UserId);
                Assert.AreEqual(entityDto.Body, result.Body);
                Assert.AreEqual(entityDto.UserName, result.UserName);

            }
        }

        [TestMethod]
        public async Task ThrowWhen_DtoPassedIsNull()
        {

            //Arrange
            var options = TestUtilities.GetOptions(nameof(ThrowWhen_DtoPassedIsNull));

            var mapperMock = new Mock<IDtoMapper<CocktailComment, CocktailCommentDto>>();

            mapperMock.Setup(x => x.MapFrom(It.IsAny<CocktailComment>())).Returns(It.IsAny<CocktailCommentDto>);

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new CocktailCommentService(assertContext, mapperMock.Object);
                await Assert.ThrowsExceptionAsync<BusinessLogicException>(() => sut.CreateAsync(null));
            }
        }
    }
}
