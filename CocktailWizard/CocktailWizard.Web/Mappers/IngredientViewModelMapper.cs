using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Web.Mappers.Contracts;
using CocktailWizard.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace CocktailWizard.Web.Mappers
{
    public class IngredientViewModelMapper : IViewModelMapper<IngredientDto, IngredientViewModel>
    {
        public IngredientViewModel MapFrom(IngredientDto dtoEntity)
        {
            if (dtoEntity == null)
            {
                return null;
            }

            return new IngredientViewModel
            {
                Id = dtoEntity.Id,
                Name = dtoEntity.Name,
            };
        }

        public ICollection<IngredientViewModel> MapFrom(ICollection<IngredientDto> dtoEntities)
        {
            return dtoEntities.Select(this.MapFrom).ToList();
        }

        public IngredientDto MapFrom(IngredientViewModel entityVM)
        {
            if (entityVM == null)
            {
                return null;
            }

            return new IngredientDto
            {
                Id = entityVM.Id,
                Name = entityVM.Name,
            };
        }

        public ICollection<IngredientDto> MapFrom(ICollection<IngredientViewModel> entitiesVM)
        {
            return entitiesVM.Select(this.MapFrom).ToList();
        }
    }
}
