using CocktailWizard.Services.ConstantMessages;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Web.Areas.Member.Models;
using CocktailWizard.Web.Mappers.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace CocktailWizard.Web.Mappers
{
    public class BarRatingViewModelMapper : IViewModelMapper<BarRatingDto, BarRatingViewModel>
    {
        public BarRatingViewModel MapFrom(BarRatingDto dtoEntity)
        {
            if (dtoEntity == null)
            {
                throw new BusinessLogicException(ExceptionMessages.DtoEntityNull);
            }

            return new BarRatingViewModel
            {
                Value = dtoEntity.Value,
                UserId = dtoEntity.UserId,
                UserName = dtoEntity.UserName,
                BarId = dtoEntity.BarId,
                CreatedOn = dtoEntity.CreatedOn,
                ModifiedOn = dtoEntity.ModifiedOn,
                DeletedOn = dtoEntity.DeletedOn,
                IsDeleted = dtoEntity.IsDeleted
            };
        }

        public ICollection<BarRatingViewModel> MapFrom(ICollection<BarRatingDto> dtoEntities)
        {
            return dtoEntities.Select(this.MapFrom).ToList();
        }

        public BarRatingDto MapFrom(BarRatingViewModel entityVM)
        {
            if (entityVM == null)
            {
                throw new BusinessLogicException(ExceptionMessages.EntityVmNull);
            }

            return new BarRatingDto
            {
                Value = entityVM.Value,
                UserId = entityVM.UserId,
                UserName = entityVM.UserName,
                BarId = entityVM.BarId,
                CreatedOn = entityVM.CreatedOn,
                ModifiedOn = entityVM.ModifiedOn,
                DeletedOn = entityVM.DeletedOn,
                IsDeleted = entityVM.IsDeleted
            };
        }

        public ICollection<BarRatingDto> MapFrom(ICollection<BarRatingViewModel> entitiesVM)
        {
            return entitiesVM.Select(this.MapFrom).ToList();
        }
    }
}
