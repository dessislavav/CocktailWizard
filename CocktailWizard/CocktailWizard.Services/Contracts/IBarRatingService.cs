using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CocktailWizard.Data.DtoEntities;

namespace CocktailWizard.Services.Contracts
{
    public interface IBarRatingService
    {
        Task<BarRatingDto> CreateAsync(BarRatingDto tempBarRating);
        Task<ICollection<BarRatingDto>> GetAllRatingsAsync(Guid barId);
    }
}