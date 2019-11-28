using CocktailWizard.Data.Entities;
using CocktailWizard.Services.ConstantMessages;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Services.DtoMappers.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace CocktailWizard.Services.DtoMappers
{
    public class DetailsCocktailDtoMapper : IDtoMapper<Cocktail, DetailsCocktailDto>
    {
        public DetailsCocktailDto MapFrom(Cocktail entity)
        {
            if (entity == null)
            {
                throw new BusinessLogicException(ExceptionMessages.EntityNull);
            }

            return new DetailsCocktailDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Info = entity.Info,
                ImagePath = entity.ImagePath,
                AverageRating = entity.Ratings
                        .Any() ? entity.Ratings
                        .Average(x => x.Value) : 0.00,
            };
        }

        public ICollection<DetailsCocktailDto> MapFrom(ICollection<Cocktail> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }

    }
}
