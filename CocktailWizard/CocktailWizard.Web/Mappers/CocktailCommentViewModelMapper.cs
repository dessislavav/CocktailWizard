using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Services.ConstantMessages;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Web.Areas.Member.Models;
using CocktailWizard.Web.Mappers.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace CocktailWizard.Web.Mappers
{
    public class CocktailCommentViewModelMapper : IViewModelMapper<CocktailCommentDto, CocktailCommentViewModel>
    {
        public CocktailCommentViewModel MapFrom(CocktailCommentDto dtoEntity)
        {
            if (dtoEntity == null)
            {
                throw new BusinessLogicException(ExceptionMessages.DtoEntityNull);
            }

            return new CocktailCommentViewModel
            {
                Id = dtoEntity.Id,
                CocktailId = dtoEntity.CocktailId,
                UserId = dtoEntity.UserId,
                UserName = dtoEntity.UserName,
                Body = dtoEntity.Body,
                CreatedOn = dtoEntity.CreatedOn,
                ModifiedOn = dtoEntity.ModifiedOn,
                DeletedOn = dtoEntity.DeletedOn,
                IsDeleted = dtoEntity.IsDeleted
            };
        }

        public ICollection<CocktailCommentViewModel> MapFrom(ICollection<CocktailCommentDto> dtoEntities)
        {
            return dtoEntities.Select(this.MapFrom).ToList();
        }

        public CocktailCommentDto MapFrom(CocktailCommentViewModel entityVM)
        {
            if (entityVM == null)
            {
                throw new BusinessLogicException(ExceptionMessages.EntityVmNull);
            }

            return new CocktailCommentDto
            {
                Id = entityVM.Id,
                CocktailId = entityVM.CocktailId,
                UserId = entityVM.UserId,
                UserName = entityVM.UserName,
                Body = entityVM.Body,
                CreatedOn = entityVM.CreatedOn,
                ModifiedOn = entityVM.ModifiedOn,
                DeletedOn = entityVM.DeletedOn,
                IsDeleted = entityVM.IsDeleted
            };
        }

        public ICollection<CocktailCommentDto> MapFrom(ICollection<CocktailCommentViewModel> entitiesVM)
        {
            return entitiesVM.Select(this.MapFrom).ToList();
        }
    }
}
