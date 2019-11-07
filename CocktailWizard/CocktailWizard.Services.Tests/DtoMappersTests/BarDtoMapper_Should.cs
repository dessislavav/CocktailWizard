using CocktailWizard.Services.DtoMappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailWizard.Services.Tests.DtoMappersTests
{
    [TestClass]
    public class BarDtoMapper_Should
    {

    }
}


//return new BarDto
//{
//    Id = entity.Id,
//    Name = entity.Name,
//    Info = entity.Info,
//    Address = entity.Address,
//    ImagePath = entity.ImagePath,
//    Phone = entity.Phone,
//    GoogleMapsURL = entity.GoogleMapsURL,
//    AverageRating = entity.Ratings
//           .Any()? entity.Ratings
//           .Average(x => x.Value) : 0.00,
//};