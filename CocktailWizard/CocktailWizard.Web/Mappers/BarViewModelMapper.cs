using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Web.Mappers.Contracts;
using CocktailWizard.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace CocktailWizard.Web.Mappers
{
    public class BarViewModelMapper : IViewModelMapper<BarDto, BarViewModel>
    {
        public BarViewModel MapFrom(BarDto dtoEntity)
        {
            if (dtoEntity == null)
            {
                return null;
            }

            return new BarViewModel
            {
                Id = dtoEntity.Id,
                Name = dtoEntity.Name,
                Address = dtoEntity.Address,
                Info = dtoEntity.Info,
                ImagePath = dtoEntity.ImagePath,
                Phone = dtoEntity.Phone,
                AverageRating = dtoEntity.AverageRating,
                GoogleMapsURL = dtoEntity.GoogleMapsURL,
            };
        }

        public ICollection<BarViewModel> MapFrom(ICollection<BarDto> dtoEntities)
        {
            return dtoEntities.Select(this.MapFrom).ToList();
        }

        public BarDto MapFrom(BarViewModel entityVM)
        {
            if (entityVM == null)
            {
                return null;
            }

            return new BarDto
            {
                Id = entityVM.Id,
                Name = entityVM.Name,
                Address = entityVM.Address,
                Info = entityVM.Info,
                ImagePath = entityVM.ImagePath,
                Phone = entityVM.Phone,
                AverageRating = entityVM.AverageRating,
                GoogleMapsURL = entityVM.GoogleMapsURL,
            };
        }

        public ICollection<BarDto> MapFrom(ICollection<BarViewModel> entitiesVM)
        {
            return entitiesVM.Select(this.MapFrom).ToList();
        }
    }
}
