﻿using CocktailWizard.Data.Entities;
using CocktailWizard.Services.ConstantMessages;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Services.DtoMappers.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace CocktailWizard.Services.DtoMappers
{
    public class BarRatingDtoMapper : IDtoMapper<BarRating, BarRatingDto>
    {
        public BarRatingDto MapFrom(BarRating entity)
        {
            if (entity == null)
            {
                throw new BusinessLogicException(ExceptionMessages.EntityNull);
            }

            return new BarRatingDto
            {
                Value = entity.Value,
                UserId = entity.UserId,
                UserName = entity.User.Email.Split('@')[0],
                BarId = entity.BarId,
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn,
                DeletedOn = entity.DeletedOn,
                IsDeleted = entity.IsDeleted
            };
        }

        public ICollection<BarRatingDto> MapFrom(ICollection<BarRating> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }
    }
}
