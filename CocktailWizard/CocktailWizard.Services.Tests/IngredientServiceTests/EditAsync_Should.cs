using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoMappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Tests.IngredientServiceTests
{
    [TestClass]
    public class EditAsync_Should
    {
        //[TestMethod]
        //public async Task ThrowWhen_IdIsNull()
        //{
        //    //Arrange
        //    var options = TestUtilities.GetOptions(nameof(ThrowWhen_IdIsNull));
        //    var mapper = new IngredientDtoMapper();

        //    using (var assertContext = new CWContext(options))
        //    {
        //        //Act & Assert
        //        var sut = new IngredientService(assertContext, mapper);
        //        await Assert.ThrowsExceptionAsync<BusinessLogicException>(() => sut.EditAsync(null, "testName"));
        //    }
        //}

        //[TestMethod]
        //public async Task ThrowWhen_NewNameIsEmptyString()
        //{
        //    //Arrange
        //    var options = TestUtilities.GetOptions(nameof(ThrowWhen_NewNameIsEmptyString));
        //    var mapper = new IngredientDtoMapper();

        //    using (var assertContext = new CWContext(options))
        //    {
        //        //Act & Assert
        //        var sut = new IngredientService(assertContext, mapper);
        //        await Assert.ThrowsExceptionAsync<BusinessLogicException>(() => sut.EditAsync(Guid.NewGuid(), String.Empty));
        //    }
        //}

        //[TestMethod]
        //public async Task SetCorrectParam_WhenValueIsValid()
        //{
        //    //Arrange
        //    var options = TestUtilities.GetOptions(nameof(SetCorrectParam_WhenValueIsValid));
        //    var mapper = new IngredientDtoMapper();
        //    var testGuid = Guid.NewGuid();

        //    var entity = new Ingredient
        //    {
        //        Id = testGuid,
        //        Name = "djodjan",
        //        IsDeleted = false
        //    };

        //    using (var assertContext = new CWContext(options))
        //    {
        //        //Act & Assert
        //        var sut = new IngredientService(assertContext, mapper);
        //        var result = sut.EditAsync(testGuid, "newDjodjan");
        //        Assert.AreEqual("newDjodjan", entity);
        //    }
        //}
    }
}