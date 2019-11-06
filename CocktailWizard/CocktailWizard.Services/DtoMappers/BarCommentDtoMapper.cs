using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
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
                return null;
            }

            return new BarCommentDto
            {
                BarId = entity.BarId,
                UserId = entity.UserId,
                //UserName = entity.User.Email,
                Body = entity.Body          
            };
        }

        public ICollection<BarCommentDto> MapFrom(ICollection<BarComment> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }
    }
}
