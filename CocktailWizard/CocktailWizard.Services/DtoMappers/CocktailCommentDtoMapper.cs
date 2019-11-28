using CocktailWizard.Data.Entities;
using CocktailWizard.Services.ConstantMessages;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Services.DtoMappers.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace CocktailWizard.Services.DtoMappers
{
    public class CocktailCommentDtoMapper : IDtoMapper<CocktailComment, CocktailCommentDto>
    {
        public CocktailCommentDto MapFrom(CocktailComment entity)
        {
            if (entity == null)
            {
                throw new BusinessLogicException(ExceptionMessages.EntityNull);
            }

            return new CocktailCommentDto
            {
                Id = entity.Id,
                CocktailId = entity.CocktailId,
                UserId = entity.UserId,
                UserName = entity.User.Email.Split('@')[0],
                Body = entity.Body,
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn,
                DeletedOn = entity.DeletedOn,
                IsDeleted = entity.IsDeleted
            };
        }

        public ICollection<CocktailCommentDto> MapFrom(ICollection<CocktailComment> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }
    }
}
