using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.CocktailCommentServiceTests
{
    [TestClass]
    public class GetCocktailCommentsAsync_Should
    {
        [TestMethod]
        public async Task ReturnCorrectInstanceOfCollectionCocktailCommentDtos()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectInstanceOfCollectionCocktailCommentDtos));

            var mapperMock = new Mock<IDtoMapper<CocktailComment, CocktailCommentDto>>();

            var id = Guid.NewGuid();
            var idTwo = Guid.NewGuid();
            var cocktailId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var entity = new CocktailComment
            {
                Id = id,
                CocktailId = cocktailId,
                UserId = userId,
                Body = "testbody",
            };

            var entityTwo = new CocktailComment
            {
                Id = idTwo,
                CocktailId = cocktailId,
                UserId = userId,
                Body = "testbodytwo",
            };

            var list = new List<CocktailCommentDto>()
            {
                new CocktailCommentDto{ Id = id, Body = "testbody" },
                new CocktailCommentDto { Id = idTwo, Body = "testbodytwo"},
            };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<ICollection<CocktailComment>>())).Returns(list);

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.CocktailComments.AddAsync(entity);
                await arrangeContext.CocktailComments.AddAsync(entityTwo);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new CocktailCommentService(assertContext, mapperMock.Object);

                var result = await sut.GetCocktailCommentsAsync(cocktailId);

                Assert.IsInstanceOfType(result, typeof(ICollection<CocktailCommentDto>));
                Assert.AreEqual(2, result.Count());
                Assert.AreEqual(entity.Body, result.First().Body);
                Assert.AreEqual(entity.Id, result.First().Id);
                Assert.AreEqual(entityTwo.Body, result.Last().Body);
                Assert.AreEqual(entityTwo.Id, result.Last().Id);
            }
        }
    }
}
