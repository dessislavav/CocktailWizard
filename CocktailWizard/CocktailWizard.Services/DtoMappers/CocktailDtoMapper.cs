using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.ConstantMessages;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoMappers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CocktailWizard.Services.DtoMappers
{
    public class CocktailDtoMapper : IDtoMapper<Cocktail, CocktailDto>
    {
        public CocktailDto MapFrom(Cocktail entity)
        {
            if (entity == null)
            {
                throw new BusinessLogicException(ExceptionMessages.EntityNull);
            }

            return new CocktailDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Info = entity.Info,
                ImagePath = entity.ImagePath,
                CocktailIngredients = entity.CocktailIngredients
                        .Select(x => x.Ingredient.Name)
                        .ToList(),
                AverageRating = entity.Ratings
                        .Any() ? entity.Ratings
                        .Average(x => x.Value) : 0.00,
            };
        }

        public ICollection<CocktailDto> MapFrom(ICollection<Cocktail> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }

    }
}
