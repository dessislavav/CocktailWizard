using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.ConstantMessages;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Services
{
    public class CocktailService
    {
        private readonly CWContext context;
        private readonly IDtoMapper<Cocktail, CocktailDto> dtoMapper;
        private readonly IngredientService ingredientService;
        private readonly CocktailIngredientService cocktailIngredientService;

        public CocktailService(CWContext context, IDtoMapper<Cocktail, CocktailDto> dtoMapper, IngredientService ingredientService, CocktailIngredientService cocktailIngredientService)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.dtoMapper = dtoMapper ?? throw new ArgumentNullException(nameof(dtoMapper));
            this.ingredientService = ingredientService ?? throw new ArgumentNullException(nameof(ingredientService));
            this.cocktailIngredientService = cocktailIngredientService ?? throw new ArgumentNullException(nameof(cocktailIngredientService));
        }

        public async Task<ICollection<CocktailDto>> GetAllCocktailsAsync()
        {
            var allCocktails = await this.context.Cocktails
                .Where(c => c.IsDeleted == false)
                .ToListAsync();

            var allCocktailsDtos = this.dtoMapper.MapFrom(allCocktails);

            return allCocktailsDtos;
        }

        public async Task<CocktailDto> GetCocktailAsync(Guid id)
        {
            var cocktail = await this.context.Cocktails
                .Where(c => c.IsDeleted == false)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (cocktail == null)
            {
                throw new BusinessLogicException(ExceptionMessages.CocktailNull);
            }

            var cocktailDto = this.dtoMapper.MapFrom(cocktail);

            return cocktailDto;
        }

        public async Task<CocktailDto> CreateAsync(CocktailDto tempCocktail)
        {
            if (tempCocktail == null)
            {
                throw new BusinessLogicException(ExceptionMessages.CocktailNull);
            }

            if (!tempCocktail.CocktailIngredients.Any())
            {
                throw new BusinessLogicException(ExceptionMessages.CocktailIngredientsNull);
            }

            var cocktail = new Cocktail
            {
                Name = tempCocktail.Name,
                Info = tempCocktail.Info,
                ImagePath = tempCocktail.ImagePath,
            };

            foreach (var item in tempCocktail.CocktailIngredients)
            {
                var ingredient = await this.ingredientService.GetIngredientAsync(item) ?? await this.ingredientService.CreateIngredientAsync(item);
                var cocktailIngredient = await this.cocktailIngredientService.CreateCocktailIngredient(cocktail.Id, ingredient.Id);
                cocktail.CocktailIngredients.Add(cocktailIngredient);
            }

            await this.context.Cocktails.AddAsync(cocktail);
            await this.context.SaveChangesAsync();

            var cocktailDto = this.dtoMapper.MapFrom(cocktail);
            return cocktailDto;
        }

        public async Task<CocktailDto> EditAsync(CocktailDto cocktailDto)
        {
            if (cocktailDto == null)
            {
                throw new BusinessLogicException(ExceptionMessages.CocktailNull);
            }

            var cocktail = await this.context.Cocktails
                .Where(c => c.IsDeleted == false)
                .FirstOrDefaultAsync(b => b.Id == cocktailDto.Id);

            try
            {
                cocktail.Name = cocktailDto.Name;
                cocktail.ImagePath = cocktailDto.ImagePath;
                cocktail.Info = cocktailDto.Info;

                this.context.Update(cocktail);
                await this.context.SaveChangesAsync();
                var editedCocktailDto = this.dtoMapper.MapFrom(cocktail);

                return editedCocktailDto;
            }
            catch (Exception)
            {
                throw new BusinessLogicException(ExceptionMessages.GeneralOopsMessage);
            }
        }

        public async Task<CocktailDto> DeleteAsync(Guid id)
        {
            var cocktail = await this.context.Cocktails
                .Where(c => c.IsDeleted == false)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (cocktail == null)
            {
                throw new BusinessLogicException(ExceptionMessages.CocktailNull);
            }
            cocktail.IsDeleted = true;
            cocktail.DeletedOn = DateTime.UtcNow;

            var cocktailDto = this.dtoMapper.MapFrom(cocktail);

            return cocktailDto;
        }
    }
}
