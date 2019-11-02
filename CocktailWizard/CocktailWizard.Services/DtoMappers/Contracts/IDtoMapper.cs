using System.Collections.Generic;

namespace CocktailWizard.Services.DtoMappers.Contracts
{
    public interface IDtoMapper<TEntity, TDto>
    {
        TDto MapFrom(TEntity entity);
        ICollection<TDto> MapFrom(ICollection<TEntity> entities);
    }

}
