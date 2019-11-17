using System;
using System.Threading.Tasks;
using CocktailWizard.Data.Entities;

namespace CocktailWizard.Services.Contracts
{
    public interface ICocktailIngredientService
    {
        Task<CocktailIngredient> CreateCocktailIngredientAsync(Guid cocktailId, Guid ingredientId);
    }
}