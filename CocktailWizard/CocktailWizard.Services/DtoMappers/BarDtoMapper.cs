using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.ConstantMessages;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoMappers.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace CocktailWizard.Services.DtoMappers
{
    public class BarDtoMapper : IDtoMapper<Bar, BarDto>
    {
        public BarDto MapFrom(Bar entity)
        {
            if (entity == null)
            {
                throw new BusinessLogicException(ExceptionMessages.EntityNull);
            }

            return new BarDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Info = entity.Info,
                Address = entity.Address,
                ImagePath = entity.ImagePath,
                Phone = entity.Phone,
                GoogleMapsURL = entity.GoogleMapsURL,
                AverageRating = entity.Ratings
                       .Any() ? entity.Ratings
                       .Average(x => x.Value) : 0.00,
            };
        }

        public ICollection<BarDto> MapFrom(ICollection<Bar> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }

    }
}
