using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Services
{
    public class IngredientService
    {
        private readonly CWContext context;
        private readonly IDtoMapper<Ingredient, IngredientDto> dtoMapper;

        public IngredientService(CWContext context, IDtoMapper<Ingredient, IngredientDto> dtoMapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context)); ;
            this.dtoMapper = dtoMapper ?? throw new ArgumentNullException(nameof(dtoMapper)); ;
        }

        public async Task<Ingredient> GetIngredientAsync(string param)
        {
            var ingredient = await this.context.Ingredients
                .Where(i => i.IsDeleted == false)
                .FirstOrDefaultAsync(x => x.Name.Equals(param));

            if (ingredient == null)
            {
                return null;
            }

            return ingredient;
        }

        public async Task<ICollection<IngredientDto>> GetAllIngredientsAsync()
        {
            var allIngredients = await this.context.Ingredients
                .Where(i => i.IsDeleted == false)
                .ToListAsync();
            var allDtoIngredients = this.dtoMapper.MapFrom(allIngredients);

            return allDtoIngredients;
        }

        public async Task<Ingredient> CreateIngredientAsync(string param)
        {
            var ingredient = new Ingredient
            {
                Name = param,
            };

            await this.context.Ingredients.AddAsync(ingredient);
            await this.context.SaveChangesAsync();
            return ingredient;
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
