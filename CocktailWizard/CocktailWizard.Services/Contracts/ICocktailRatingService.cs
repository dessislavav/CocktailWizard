using CocktailWizard.Services.DtoEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Contracts
{
    public interface ICocktailRatingService
    {
        Task<CocktailRatingDto> CreateAsync(CocktailRatingDto tempCocktailRating);
        Task<ICollection<CocktailRatingDto>> GetAllRatingsAsync(Guid cocktailId);
        Task<CocktailRatingDto> GetRatingAsync(Guid cocktailId, Guid userId);
    }
}