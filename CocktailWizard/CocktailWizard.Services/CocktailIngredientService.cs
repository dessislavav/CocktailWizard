using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace CocktailWizard.Services
{
    public class CocktailIngredientService : ICocktailIngredientService
    {
        private readonly CWContext context;

        public CocktailIngredientService(CWContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<CocktailIngredient> CreateCocktailIngredientAsync(Guid cocktailId, Guid ingredientId)
        {
            var cocktailIngredient = new CocktailIngredient
            {
                CocktailId = cocktailId,
                IngredientId = ingredientId,
            };

            await this.context.CocktailIngredients.AddAsync(cocktailIngredient);
            await this.context.SaveChangesAsync();

            return cocktailIngredient;
        }
    }
}
