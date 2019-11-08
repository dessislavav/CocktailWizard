using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.DtoMappers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CocktailWizard.Services.DtoMappers
{
    public class DetailsCocktailDtoMapper : IDtoMapper<Cocktail, DetailsCocktailDto>
    {
        public DetailsCocktailDto MapFrom(Cocktail entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new DetailsCocktailDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Info = entity.Info,
                ImagePath = entity.ImagePath,          
                AverageRating = entity.Ratings
                        .Any() ? entity.Ratings
                        .Average(x => x.Value) : 0.00,
            };
        }

        public ICollection<DetailsCocktailDto> MapFrom(ICollection<Cocktail> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }

    }
}
