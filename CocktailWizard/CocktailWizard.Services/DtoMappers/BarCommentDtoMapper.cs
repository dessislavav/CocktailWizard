using CocktailWizard.Data.Entities;
using CocktailWizard.Services.ConstantMessages;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Services.DtoMappers.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace CocktailWizard.Services.DtoMappers
{
    public class BarCommentDtoMapper : IDtoMapper<BarComment, BarCommentDto>
    {

        public BarCommentDto MapFrom(BarComment entity)
        {
            if (entity == null)
            {
                throw new BusinessLogicException(ExceptionMessages.EntityNull);
            }

            return new BarCommentDto
            {
                Id = entity.Id,
                BarId = entity.BarId,
                UserName = entity.User.Email.Split('@')[0],
                Body = entity.Body,
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn,
                DeletedOn = entity.DeletedOn,
                IsDeleted = entity.IsDeleted
            };
        }

        public ICollection<BarCommentDto> MapFrom(ICollection<BarComment> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }
    }
}
