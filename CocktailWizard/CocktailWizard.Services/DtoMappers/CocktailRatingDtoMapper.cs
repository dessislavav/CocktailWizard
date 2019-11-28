using CocktailWizard.Data.Entities;
using CocktailWizard.Services.ConstantMessages;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Services.DtoMappers.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace CocktailWizard.Services.DtoMappers
{
    public class CocktailRatingDtoMapper : IDtoMapper<CocktailRating, CocktailRatingDto>
    {
        public CocktailRatingDto MapFrom(CocktailRating entity)
        {
            if (entity == null)
            {
                throw new BusinessLogicException(ExceptionMessages.EntityNull);
            }

            return new CocktailRatingDto
            {
                Value = entity.Value,
                UserId = entity.UserId,
                UserName = entity.User.Email.Split('@')[0],
                CocktailId = entity.CocktailId,
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn,
                DeletedOn = entity.DeletedOn,
                IsDeleted = entity.IsDeleted

            };
        }

        public ICollection<CocktailRatingDto> MapFrom(ICollection<CocktailRating> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }
    }
}
