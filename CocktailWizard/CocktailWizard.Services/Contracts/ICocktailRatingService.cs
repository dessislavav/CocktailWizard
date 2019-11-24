using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CocktailWizard.Data.DtoEntities;

namespace CocktailWizard.Services.Contracts
{
    public interface ICocktailRatingService
    {
        Task<CocktailRatingDto> CreateAsync(CocktailRatingDto tempCocktailRating);
        Task<ICollection<CocktailRatingDto>> GetAllRatingsAsync(Guid cocktailId);
        Task<CocktailRatingDto> GetRatingAsync(Guid cocktailId, Guid userId);
    }
}