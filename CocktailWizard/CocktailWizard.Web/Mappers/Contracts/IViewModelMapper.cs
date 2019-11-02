using System.Collections.Generic;

namespace CocktailWizard.Web.Mappers.Contracts
{
    public interface IViewModelMapper<TDto, TViewModel>
    {
        TViewModel MapFrom(TDto dtoEntity);
        ICollection<TViewModel> MapFrom(ICollection<TDto> dtoEntities);
        TDto MapFrom(TViewModel entityVM);
        ICollection<TDto> MapFrom(ICollection<TViewModel> entitiesVM);
    }
}
