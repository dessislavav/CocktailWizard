using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Web.Areas.Member.Models;
using CocktailWizard.Web.Mappers.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace CocktailWizard.Web.Mappers
{
    public class CocktailRatingViewModelMapper : IViewModelMapper<CocktailRatingDto, CocktailRatingViewModel>
    {
        public CocktailRatingViewModel MapFrom(CocktailRatingDto dtoEntity)
        {
            if (dtoEntity == null)
            {
                return null;
            }

            return new CocktailRatingViewModel
            {
                Value = dtoEntity.Value,
                UserId = dtoEntity.UserId,
                UserName = dtoEntity.UserName,
                CocktailId = dtoEntity.CocktailId,
                CreatedOn = dtoEntity.CreatedOn,
                ModifiedOn = dtoEntity.ModifiedOn,
                DeletedOn = dtoEntity.DeletedOn,
                IsDeleted = dtoEntity.IsDeleted
            };
        }

        public ICollection<CocktailRatingViewModel> MapFrom(ICollection<CocktailRatingDto> dtoEntities)
        {
            return dtoEntities.Select(this.MapFrom).ToList();
        }

        public CocktailRatingDto MapFrom(CocktailRatingViewModel entityVM)
        {
            if (entityVM == null)
            {
                return null;
            }

            return new CocktailRatingDto
            {
                Value = entityVM.Value,
                UserId = entityVM.UserId,
                UserName = entityVM.UserName,
                CocktailId = entityVM.CocktailId,
                CreatedOn = entityVM.CreatedOn,
                ModifiedOn = entityVM.ModifiedOn,
                DeletedOn = entityVM.DeletedOn,
                IsDeleted = entityVM.IsDeleted
            };
        }

        public ICollection<CocktailRatingDto> MapFrom(ICollection<CocktailRatingViewModel> entitiesVM)
        {
            return entitiesVM.Select(this.MapFrom).ToList();
        }
    }
}
