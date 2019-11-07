using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Web.Mappers.Contracts;
using CocktailWizard.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace CocktailWizard.Web.Mappers
{
    public class BarCommentViewModelMapper : IViewModelMapper<BarCommentDto, BarCommentViewModel>
    {
        public BarCommentViewModel MapFrom(BarCommentDto dtoEntity)
        {
            if (dtoEntity == null)
            {
                return null;
            }

            return new BarCommentViewModel
            {
                BarId = dtoEntity.BarId,
                UserId = dtoEntity.UserId,
                UserName = dtoEntity.UserName,
                Body = dtoEntity.Body,
                CreatedOn = dtoEntity.CreatedOn,
                ModifiedOn = dtoEntity.ModifiedOn,
                DeletedOn = dtoEntity.DeletedOn,
                IsDeleted = dtoEntity.IsDeleted,
                
            };
        }

        public ICollection<BarCommentViewModel> MapFrom(ICollection<BarCommentDto> dtoEntities)
        {
            return dtoEntities.Select(this.MapFrom).ToList();
        }

        public BarCommentDto MapFrom(BarCommentViewModel entityVM)
        {
            if (entityVM == null)
            {
                return null;
            }

            return new BarCommentDto
            {
                BarId = entityVM.BarId,
                UserId = entityVM.UserId,
                UserName = entityVM.UserName,
                Body = entityVM.Body
            };
        }

        public ICollection<BarCommentDto> MapFrom(ICollection<BarCommentViewModel> entitiesVM)
        {
            return entitiesVM.Select(this.MapFrom).ToList();
        }
    }
}
