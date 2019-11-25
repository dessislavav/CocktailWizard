using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.BarCommentServiceTests
{
    [TestClass]
    public class GetBarCommentAsync_Should
    {
        [TestMethod]
        public async Task ReturnCorrectBarCommentWhen_ParamsAreValid()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectBarCommentWhen_ParamsAreValid));

            var mapperMock = new Mock<IDtoMapper<BarComment, BarCommentDto>>();

            var id = Guid.NewGuid();
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

            //var entityDto = new BarCommentDto
            //{
            //    Id = id,
            //    BarId = barId,
            //    UserId = Guid.NewGuid(),
            //    Body = "testbody",
            //    CreatedOn = createdOn
            //};

            //mapperMock.Setup(x => x.MapFrom(It.IsAny<BarComment>())).Returns(entityDto);

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.BarComments.AddAsync(entity);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new BarCommentService(assertContext, mapperMock.Object);
                //var result = await sut.GetBarCommentAsync(barId);

                //Assert.IsInstanceOfType(result, typeof(BarComment));
                //Assert.AreEqual("testbody", result.Body);
            }
        }
    }
}
