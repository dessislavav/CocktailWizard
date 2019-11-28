using CocktailWizard.Services.DtoEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Contracts
{
    public interface IBarRatingService
    {
        Task<BarRatingDto> CreateAsync(BarRatingDto tempBarRating);
        Task<ICollection<BarRatingDto>> GetAllRatingsAsync(Guid barId);
        Task<BarRatingDto> GetRatingAsync(Guid barId, Guid userId);
    }
}