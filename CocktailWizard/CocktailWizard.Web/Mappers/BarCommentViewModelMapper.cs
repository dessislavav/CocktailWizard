using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Services.ConstantMessages;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Web.Areas.Member.Models;
using CocktailWizard.Web.Mappers.Contracts;
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
                throw new BusinessLogicException(ExceptionMessages.DtoEntityNull);
            }

            return new BarCommentViewModel
            {
                Id = dtoEntity.Id,
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
                throw new BusinessLogicException(ExceptionMessages.EntityVmNull);
            }

            return new BarCommentDto
            {
                Id = entityVM.Id,
                BarId = entityVM.BarId,
                UserId = entityVM.UserId,
                UserName = entityVM.UserName,
                Body = entityVM.Body,
                CreatedOn = entityVM.CreatedOn,
                ModifiedOn = entityVM.ModifiedOn,
                DeletedOn = entityVM.DeletedOn,
                IsDeleted = entityVM.IsDeleted
            };
        }

        public ICollection<BarCommentDto> MapFrom(ICollection<BarCommentViewModel> entitiesVM)
        {
            return entitiesVM.Select(this.MapFrom).ToList();
        }
    }
}
