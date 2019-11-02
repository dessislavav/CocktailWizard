using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.DtoMappers.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace CocktailWizard.Services.DtoMappers
{
    public class BarDtoMapper : IDtoMapper<Bar, BarDto>
    {
        public BarDto MapFrom(Bar entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new BarDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Address = entity.Address,
                ImagePath = entity.ImagePath,
                Phone = entity.Phone

            };
        }

        public ICollection<BarDto> MapFrom(ICollection<Bar> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }

    }
}
