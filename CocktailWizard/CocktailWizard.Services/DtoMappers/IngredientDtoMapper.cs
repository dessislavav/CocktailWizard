using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.DtoMappers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CocktailWizard.Services.DtoMappers
{
    public class IngredientDtoMapper : IDtoMapper<Ingredient, IngredientDto>
    {
        public IngredientDto MapFrom(Ingredient entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new IngredientDto
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }

        public ICollection<IngredientDto> MapFrom(ICollection<Ingredient> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }

    }
}
