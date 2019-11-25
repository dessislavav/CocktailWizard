using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Services.ConstantMessages;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Web.Mappers.Contracts;
using CocktailWizard.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace CocktailWizard.Web.Mappers
{
    public class CocktailViewModelMapper : IViewModelMapper<CocktailDto, CocktailViewModel>
    {
        public CocktailViewModel MapFrom(CocktailDto dtoEntity)
        {
            if (dtoEntity == null)
            {
                throw new BusinessLogicException(ExceptionMessages.DtoEntityNull);
            }

            return new CocktailViewModel
            {
                Id = dtoEntity.Id,
                Name = dtoEntity.Name,
                Info = dtoEntity.Info,
                ImagePath = dtoEntity.ImagePath,
                CocktailIngredients = dtoEntity.CocktailIngredients,
                AverageRating = dtoEntity.AverageRating,
            };
        }

        public ICollection<CocktailViewModel> MapFrom(ICollection<CocktailDto> dtoEntities)
        {
            return dtoEntities.Select(this.MapFrom).ToList();
        }

        public CocktailDto MapFrom(CocktailViewModel entityVM)
        {
            if (entityVM == null)
            {
                throw new BusinessLogicException(ExceptionMessages.EntityVmNull);
            }

            return new CocktailDto
            {
                Id = entityVM.Id,
                Name = entityVM.Name,
                Info = entityVM.Info,
                ImagePath = entityVM.ImagePath,
                CocktailIngredients = entityVM.CocktailIngredients,
                AverageRating = entityVM.AverageRating.Value,
            };
        }

        public ICollection<CocktailDto> MapFrom(ICollection<CocktailViewModel> entitiesVM)
        {
            return entitiesVM.Select(this.MapFrom).ToList();
        }
    }
}
