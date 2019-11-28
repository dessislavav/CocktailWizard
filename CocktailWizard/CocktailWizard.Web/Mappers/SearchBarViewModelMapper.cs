using CocktailWizard.Services.ConstantMessages;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Web.Mappers.Contracts;
using CocktailWizard.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace CocktailWizard.Web.Mappers
{
    public class SearchBarViewModelMapper : IViewModelMapper<SearchBarDto, BarViewModel>
    {
        public BarViewModel MapFrom(SearchBarDto dtoEntity)
        {
            if (dtoEntity == null)
            {
                throw new BusinessLogicException(ExceptionMessages.DtoEntityNull);
            }

            return new BarViewModel
            {
                Id = dtoEntity.Id,
                Name = dtoEntity.Name,
                Address = dtoEntity.Address,
                ImagePath = dtoEntity.ImagePath,
                AverageRating = dtoEntity.AverageRating,
            };
        }

        public ICollection<BarViewModel> MapFrom(ICollection<SearchBarDto> dtoEntities)
        {
            return dtoEntities.Select(this.MapFrom).ToList();
        }

        public SearchBarDto MapFrom(BarViewModel entityVM)
        {
            if (entityVM == null)
            {
                throw new BusinessLogicException(ExceptionMessages.EntityVmNull);
            }

            return new SearchBarDto
            {
                Id = entityVM.Id,
                Name = entityVM.Name,
                Address = entityVM.Address,
                ImagePath = entityVM.ImagePath,
                AverageRating = entityVM.AverageRating,
            };
        }

        public ICollection<SearchBarDto> MapFrom(ICollection<BarViewModel> entitiesVM)
        {
            return entitiesVM.Select(this.MapFrom).ToList();
        }
    }
}
