using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.Contracts;
using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Services.DtoMappers;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.CocktailServiceTests
{
    [TestClass]
    public class RemoveBarsAsync_Should
    {
        [TestMethod]
        public async Task CorrectlyRemoveBarsWhen_ParamsAreValid()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(CorrectlyRemoveBarsWhen_ParamsAreValid));
            var mapper = new CocktailDtoMapper();

            var mapperMock = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var barMapperMock = new Mock<IDtoMapper<Bar, BarDto>>();
            var cocktailDetailsMapperMock = new Mock<IDtoMapper<Cocktail, DetailsCocktailDto>>();

            var ingredientServiceMock = new Mock<IIngredientService>();
            var cocktailIngredientServiceMock = new Mock<ICocktailIngredientService>();

            var guid = Guid.NewGuid();
            var barGuid = Guid.NewGuid();

            var cocktail = new Cocktail
            {
                Id = guid,
                Name = "testCocktail",
                Info = "testCocktailInfo",
            };

            var entityDto = new CocktailDto
            {
                Id = guid,
                Name = "testCocktail",
                Info = "testInfo",
            };

            var list = new List<string> { "Baileys" };

            var bar = new Bar { Id = barGuid, Name = "Baileys" };
            var barCocktail = new BarCocktail { BarId = barGuid, CocktailId = cocktail.Id };

            mapperMock.Setup(x => x.MapFrom(It.IsAny<Cocktail>())).Returns(entityDto);

            using (var arrangeContext = new CWContext(options))
            {
                await arrangeContext.Cocktails.AddAsync(cocktail);
                await arrangeContext.Bars.AddAsync(bar);
                await arrangeContext.BarCocktails.AddAsync(barCocktail);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CWContext(options))
            {
                //Act & Assert
                var sut = new CocktailService(assertContext, mapperMock.Object, barMapperMock.Object,
                    cocktailDetailsMapperMock.Object, ingredientServiceMock.Object, cocktailIngredientServiceMock.Object);

                var result = await sut.RemoveBarsAsync(entityDto, list);

                var barCocktailEntity = await assertContext.BarCocktails.FirstAsync();
                Assert.IsInstanceOfType(result, typeof(CocktailDto));
                Assert.AreEqual("testCocktail", result.Name);
                Assert.AreEqual(true, barCocktailEntity.IsDeleted);
            }
        }
    }
}
