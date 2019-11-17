using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.BanServiceTests
{
    [TestClass]
    public class RemoveAsync_Should
    {
        [TestMethod]
        public async Task CorrectlyRemoveBan_WhenParamsAreValid()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(CorrectlyRemoveBan_WhenParamsAreValid));
            var mapperMock = new Mock<IDtoMapper<User, UserDto>>();
            var testGuid = Guid.NewGuid();
            var userTest = new User { Id = testGuid, UserName = "Pesho", IsBanned = true, LockoutEnabled = true, LockoutEnd = DateTime.UtcNow.AddDays(1) };
            var banTest = new Ban { HasExpired = false, User = userTest };

            using (var actContext = new CWContext(options))
            {
                //Act
                await actContext.Users.AddAsync(userTest);
                await actContext.Bans.AddAsync(banTest);
                await actContext.SaveChangesAsync();
                var sut = new BanService(actContext, mapperMock.Object);
                await sut.RemoveAsync(testGuid);
                await actContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Assert
                var user = await assertContext.Users.FirstAsync();
                var ban = assertContext.Bans
                    .Include(u => u.User)
                    .Where(b => b.User == user)
                    .FirstOrDefault();
                Assert.AreEqual(user.IsBanned, false);
                Assert.AreEqual(ban.HasExpired, true);
                Assert.AreEqual(user.LockoutEnd < DateTime.Now, true);
            }
        }
    }
}
