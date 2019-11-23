using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.BarCommentServiceTests
{
    [TestClass]
    public class CreateAsync_Should
    {
        [TestMethod]
        public async Task CorrectlyCreateBarComment()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(CorrectlyCreateBarComment));

            var mapperMock = new Mock<IDtoMapper<BarComment, BarCommentDto>>();

            var id = Guid.NewGuid();
            var barId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var createdOn = DateTime.UtcNow;

            var entityDto = new BarCommentDto
            {
                Id = id,
                BarId = barId,
                UserId = userId,
                UserName = "testuser",
                Body = "testbody",
                CreatedOn = createdOn
            };

            var entity = new BarComment
            { 

                Id = id,
                BarId = barId,
                UserId = userId,
                Body = "testbody",
                CreatedOn = createdOn
            };

        mapperMock.Setup(x => x.MapFrom(It.IsAny<BarComment>())).Returns(entityDto);


            using (var assertContext = new CWContext(options))
            {
                //Assert

                var sut = new BarCommentService(assertContext, mapperMock.Object);
                var result = await sut.CreateAsync(entityDto);

                Assert.IsInstanceOfType(result, typeof(BarCommentDto));
                Assert.AreEqual(id, result.Id);
                Assert.AreEqual(barId, result.BarId);
                Assert.AreEqual(userId, result.UserId);
                Assert.AreEqual("testbody", result.Body);
                Assert.AreEqual("testuser", result.UserName);

                Assert.AreEqual(entityDto.Id, result.Id);
                Assert.AreEqual(entityDto.BarId, result.BarId);
                Assert.AreEqual(entityDto.UserId, result.UserId);
                Assert.AreEqual(entityDto.Body, result.Body);
                Assert.AreEqual(entityDto.UserName, result.UserName);

            }
        }

        [TestMethod]
        public async Task ThrowWhen_DtoPassedIsNull()
        {
            
            //Arrange
            var options = TestUtilities.GetOptions(nameof(CorrectlyCreateBarComment));

            var mapperMock = new Mock<IDtoMapper<BarComment, BarCommentDto>>();

            mapperMock.Setup(x => x.MapFrom(It.IsAny<BarComment>())).Returns(It.IsAny<BarCommentDto>);

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarCommentService(assertContext, mapperMock.Object);
                await Assert.ThrowsExceptionAsync<BusinessLogicException>(() => sut.CreateAsync(null));
            }
        }
    }
}
