using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.BarCommentServiceTests
{
    [TestClass]
    public class GetBarCommentsAsync_Should
    {
        [TestMethod]
        public async Task ReturnInstanceOfCollectionBarCommentDtos()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnInstanceOfCollectionBarCommentDtos));

            var mapperMock = new Mock<IDtoMapper<BarComment, BarCommentDto>>();

            var id = Guid.NewGuid();
            var twoId = Guid.NewGuid();
            var barId = Guid.NewGuid();
            var createdOn = DateTime.UtcNow;

            var entity = new BarComment
            {
                Id = id,
                BarId = barId,
                UserId = Guid.NewGuid(),
                Body = "testbody",
                CreatedOn = createdOn
            };

            var entityTwo = new BarComment
            {
                Id = twoId,
                BarId = barId,
                UserId = Guid.NewGuid(),
                Body = "testbodytwo",
                CreatedOn = createdOn
            };

            var list = new List<BarCommentDto>()
            {
                new BarCommentDto{ Id = id, Body = "testbody" },
                new BarCommentDto { Id = twoId, Body = "testbodytwo"},
            };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<ICollection<BarComment>>())).Returns(list);

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.BarComments.AddAsync(entity);
                await arrangeContext.BarComments.AddAsync(entityTwo);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarCommentService(assertContext, mapperMock.Object);

                var result = await sut.GetBarCommentsAsync(barId);

                Assert.IsInstanceOfType(result, typeof(ICollection<BarCommentDto>));
                Assert.AreEqual(2, result.Count());
                Assert.AreEqual(entity.Body, result.First().Body);
                Assert.AreEqual(entity.Id, result.First().Id);
                Assert.AreEqual(entityTwo.Body, result.Last().Body);
                Assert.AreEqual(entityTwo.Id, result.Last().Id);
            }

        }
    }
}
