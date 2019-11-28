using CocktailWizard.Data.Entities;
using CocktailWizard.Services.ConstantMessages;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Services.DtoMappers.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace CocktailWizard.Services.DtoMappers
{
    public class IngredientDtoMapper : IDtoMapper<Ingredient, IngredientDto>
    {
        public IngredientDto MapFrom(Ingredient entity)
        {
            if (entity == null)
            {
                throw new BusinessLogicException(ExceptionMessages.EntityNull);
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
