using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.Contracts;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.BarCommentServiceTests
{
    [TestClass]
    public class DeleteAsync_Should
    {
        [TestMethod]
        public async Task CorrectlyDeleteBarComment()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(CorrectlyDeleteBarComment));

            var mapperMock = new Mock<IDtoMapper<BarComment, BarCommentDto>>();

            var id = Guid.NewGuid();
            var barId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var createdOn = DateTime.UtcNow;

            var bar = new Bar
            {
                Id = barId,
                Name = "testname"
            };

            var user = new User
            {
                Id = userId,
                UserName = "testUser",
            };

            var entity = new BarComment
            {
                Id = id,
                BarId = barId,
                Bar = bar,
                UserId = Guid.NewGuid(),
                User = user,
                Body = "testbody",
                CreatedOn = createdOn
            };

            bar.Comments.Add(entity);
            user.BarComments.Add(entity);

            using (var actContext = new CWContext(options))
            {
                //Act
                await actContext.Bars.AddAsync(bar);
                await actContext.Users.AddAsync(user);
                await actContext.BarComments.AddAsync(entity);
                await actContext.SaveChangesAsync();
                var service = new BarCommentService(actContext, mapperMock.Object);
                var result = await service.DeleteAsync(id, barId);
                await actContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Assert
                var result = await assertContext.BarComments.FirstAsync();
                Assert.AreEqual(true, result.IsDeleted);
            }
        }


        [TestMethod]
        public async Task ThrowWhen_NoCommentFound()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ThrowWhen_NoCommentFound));

            var mapperMock = new Mock<IDtoMapper<BarComment, BarCommentDto>>();

            var id = Guid.NewGuid();
            var barId = Guid.NewGuid();

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarCommentService(assertContext, mapperMock.Object);
                await Assert.ThrowsExceptionAsync<BusinessLogicException>(() => sut.DeleteAsync(id, barId));
            }
        }
    }
}
