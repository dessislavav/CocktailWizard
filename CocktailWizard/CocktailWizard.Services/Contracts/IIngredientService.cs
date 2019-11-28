using CocktailWizard.Data.Entities;
using CocktailWizard.Services.DtoEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Contracts
{
    public interface IIngredientService
    {
        Task<IngredientDto> CreateIngredientAsync(IngredientDto ingredientDto);
        Task<Ingredient> CreateIngredientAsync(string param);
        Task<IngredientDto> DeleteAsync(Guid id);
        Task<IngredientDto> EditAsync(Guid id, string newName);
        Task<IngredientDto> GetIngredientAsync(Guid id);
        Task<Ingredient> GetIngredientAsync(string param);
        Task<ICollection<IngredientDto>> GetIngredientsAsync();
        Task<ICollection<IngredientDto>> GetIngredientsAsync(int count);
        Task<int> GetPageCountAsync(int ingredientsPerPage);
        Task<ICollection<IngredientDto>> GetTenIngredientsOrderedByNameAsync(int currentPage);
        //why no merge
    }
}