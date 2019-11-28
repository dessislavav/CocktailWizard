using CocktailWizard.Services.ConstantMessages;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Web.Mappers.Contracts;
using CocktailWizard.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace CocktailWizard.Web.Mappers
{
    public class DetailsCocktailViewModelMapper : IViewModelMapper<DetailsCocktailDto, DetailsCocktailViewModel>
    {
        public DetailsCocktailViewModel MapFrom(DetailsCocktailDto dtoEntity)
        {
            if (dtoEntity == null)
            {
                throw new BusinessLogicException(ExceptionMessages.DtoEntityNull);
            }

            return new DetailsCocktailViewModel
            {
                Id = dtoEntity.Id,
                Name = dtoEntity.Name,
                Info = dtoEntity.Info,
                ImagePath = dtoEntity.ImagePath,
                AverageRating = dtoEntity.AverageRating,
            };
        }

        public ICollection<DetailsCocktailViewModel> MapFrom(ICollection<DetailsCocktailDto> dtoEntities)
        {
            return dtoEntities.Select(this.MapFrom).ToList();
        }

        public DetailsCocktailDto MapFrom(DetailsCocktailViewModel entityVM)
        {
            if (entityVM == null)
            {
                throw new BusinessLogicException(ExceptionMessages.EntityVmNull);
            }

            return new DetailsCocktailDto
            {
                Id = entityVM.Id,
                Name = entityVM.Name,
                Info = entityVM.Info,
                ImagePath = entityVM.ImagePath,
                AverageRating = entityVM.AverageRating.Value,
            };
        }

        public ICollection<DetailsCocktailDto> MapFrom(ICollection<DetailsCocktailViewModel> entitiesVM)
        {
            return entitiesVM.Select(this.MapFrom).ToList();
        }
    }
}
