using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.DtoMappers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CocktailWizard.Services.DtoMappers
{
    public class SearchBarDtoMapper : IDtoMapper<Bar, SearchBarDto>
    {
        public SearchBarDto MapFrom(Bar entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new SearchBarDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Address = entity.Address,
                ImagePath = entity.ImagePath,
                AverageRating = entity.Ratings
                       .Any() ? entity.Ratings
                       .Average(x => x.Value) : 0.00,
            };
        }

        public ICollection<SearchBarDto> MapFrom(ICollection<Bar> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }

    }
}
