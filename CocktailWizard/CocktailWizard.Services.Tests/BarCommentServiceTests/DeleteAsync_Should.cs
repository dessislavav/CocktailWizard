using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.Contracts;
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
            //var barCommentService = new Mock<IBarCommentService>();

            var id = Guid.NewGuid();
            var barId = Guid.NewGuid();
            var createdOn = DateTime.UtcNow;

            var bar = new Bar
            {
                Id = barId,
                Name = "testname"
            };

            var entity = new BarComment
            {
                Id = id,
                BarId = barId,
                UserId = Guid.NewGuid(),
                Body = "testbody",
                CreatedOn = createdOn
            };

           // bar.Comments.Add()

            //barCommentService.Setup(x => x.GetBarCommentAsync(It.IsAny<Guid>()))
                //.Returns(Task.FromResult(entity));

            using (var actContext = new CWContext(options))
            {
                //Act
                await actContext.Bars.AddAsync(bar);
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
    }
}
