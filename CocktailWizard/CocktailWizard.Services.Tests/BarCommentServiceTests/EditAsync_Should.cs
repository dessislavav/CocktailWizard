using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.BarCommentServiceTests
{
    [TestClass]
    public class EditAsync_Should
    {
        [TestMethod]
        public async Task CorrectlyUpdateEntity()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(CorrectlyUpdateEntity));

            var mapperMock = new Mock<IDtoMapper<BarComment, BarCommentDto>>();

            var id = Guid.NewGuid();
            var barId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var createdOn = DateTime.UtcNow;

            var entity = new BarComment
            {

                Id = id,
                BarId = barId,
                UserId = userId,
                Body = "testbody",
                CreatedOn = createdOn
            };

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.BarComments.AddAsync(entity);
                await arrangeContext.SaveChangesAsync();
            }

            mapperMock.Setup(x => x.MapFrom(It.IsAny<BarComment>())).Returns(It.IsAny<BarCommentDto>);

            using (var assertContext = new CWContext(options))
            {
                var sut = new BarCommentService(assertContext, mapperMock.Object);

                var result = await sut.EditAsync(id, "newbody");

                var modifiedComment = await assertContext.BarComments.FirstAsync();

                Assert.AreEqual("newbody", modifiedComment.Body);

            }
        }

        [TestMethod]
        public async Task ReturnCorrectTypeOfEntity()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectTypeOfEntity));

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

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.BarComments.AddAsync(entity);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarCommentService(assertContext, mapperMock.Object);

                var result = await sut.EditAsync(id, "newbody");
                Assert.IsInstanceOfType(result, typeof(BarCommentDto));
            }
        }

        [TestMethod]
        public async Task ThrowWhen_NoCommentFound()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ThrowWhen_NoCommentFound));

            var mapperMock = new Mock<IDtoMapper<BarComment, BarCommentDto>>();

            var id = Guid.NewGuid();
            var barId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var fakeId = Guid.NewGuid();

            var createdOn = DateTime.UtcNow;

            var entity = new BarComment
            {

                Id = id,
                BarId = barId,
                UserId = userId,
                Body = "testbody",
                CreatedOn = createdOn
            };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<BarComment>())).Returns(It.IsAny<BarCommentDto>);

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.BarComments.AddAsync(entity);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarCommentService(assertContext, mapperMock.Object);

                await Assert.ThrowsExceptionAsync<BusinessLogicException>(() => sut.EditAsync(fakeId, "newbody"));
            }
        }
    }
}
