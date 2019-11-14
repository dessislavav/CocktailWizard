using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.ConstantMessages;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
        private readonly IDtoMapper<Cocktail, DetailsCocktailDto> detailsCocktailDtoMapper;
        private readonly IDtoMapper<Bar, BarDto> barDtoMapper;
        private readonly IngredientService ingredientService;
        private readonly CocktailIngredientService cocktailIngredientService;

        public CocktailService(CWContext context, IDtoMapper<Cocktail, CocktailDto> dtoMapper, IDtoMapper<Bar, BarDto> barDtoMapper, IDtoMapper<Cocktail, DetailsCocktailDto> detailsCocktailDtoMapper, IngredientService ingredientService, CocktailIngredientService cocktailIngredientService)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.dtoMapper = dtoMapper ?? throw new ArgumentNullException(nameof(dtoMapper));
            this.barDtoMapper = barDtoMapper ?? throw new ArgumentNullException(nameof(barDtoMapper));
            this.detailsCocktailDtoMapper = detailsCocktailDtoMapper ?? throw new ArgumentNullException(nameof(barDtoMapper));
            this.ingredientService = ingredientService ?? throw new ArgumentNullException(nameof(ingredientService));
            this.cocktailIngredientService = cocktailIngredientService ?? throw new ArgumentNullException(nameof(cocktailIngredientService));
        }

        public async Task<ICollection<CocktailDto>> GetTenCocktailsOrderedByNameAsync(int currentPage)
        {
            List<Cocktail> tenCocktails;
            if (currentPage == 1)
            {
                tenCocktails = await this.context.Cocktails
                    .OrderBy(x => x.Name)
                    .Take(10)
                    .ToListAsync();
            }
            else
            {
                tenCocktails = await this.context.Cocktails
                    .OrderBy(x => x.Name)
                    .Skip((currentPage - 1) * 10)
                    .Take(10)
                    .ToListAsync();
            }

            var dtoCocktails = this.dtoMapper.MapFrom(tenCocktails);

            return dtoCocktails;
        }

        public async Task<int> GetPageCountAsync(int cocktailsPerPage)
        {
            var allCocktails = await context.Cocktails.CountAsync();

            var totalPages = (allCocktails - 1) / cocktailsPerPage + 1;

            return totalPages;
        }

        public async Task<ICollection<CocktailDto>> GetAllCocktailsAsync()
        {
            var allCocktails = await this.context.Cocktails
                .Where(c => c.IsDeleted == false)
                .OrderBy(b => b.Name)
                .ToListAsync();

            var allCocktailsDtos = this.dtoMapper.MapFrom(allCocktails);

            return allCocktailsDtos;
        }

        public async Task<DetailsCocktailDto> GetCocktailsBars(Guid id)
        {
            var cocktail = await this.context.Cocktails
                .Where(c => c.IsDeleted == false)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (cocktail == null)
            {
                throw new BusinessLogicException(ExceptionMessages.CocktailNull);
            }

            var detailsCocktailDto = this.detailsCocktailDtoMapper.MapFrom(cocktail);
            var bars = await this.context.BarCocktails.Include(b => b.Bar).Where(b => b.CocktailId == cocktail.Id).Select(b => b.Bar).ToListAsync();
            var mappedBars = this.barDtoMapper.MapFrom(bars);
            detailsCocktailDto.Bars = mappedBars;

            return detailsCocktailDto;
        }

        public async Task<ICollection<CocktailDto>> GetTopCocktails(int num)
        {
            var topCocktails = await this.context.Cocktails
                    .Where(b => b.IsDeleted == false)
                    .Take(num)
                    .ToListAsync();

            if (!topCocktails.Any())
            {
                throw new BusinessLogicException(ExceptionMessages.BarNull);
            }

            var topCocktailsDtos = this.dtoMapper.MapFrom(topCocktails);

            return topCocktailsDtos;
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
                var ingredient = await this.ingredientService.GetIngredientAsync(item) ?? throw new ArgumentException(ExceptionMessages.IngredientNull);

                var cocktailIngredient = await this.cocktailIngredientService.CreateCocktailIngredientAsync(cocktail.Id, ingredient.Id);
                cocktail.CocktailIngredients.Add(cocktailIngredient);
            }

            await this.context.Cocktails.AddAsync(cocktail);
            await this.context.SaveChangesAsync();

            var cocktailDto = this.dtoMapper.MapFrom(cocktail);
            return cocktailDto;
        }

        public async Task<CocktailDto> EditAsync(Guid id, string newName, string newInfo, string newImagePath)
        {
            var cocktail = await this.context.Cocktails
                .Where(c => c.IsDeleted == false)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (cocktail == null)
            {
                throw new BusinessLogicException(ExceptionMessages.CocktailNull);
            }

            try
            {
                cocktail.Name = newName;
                cocktail.Info = newInfo;
                cocktail.ImagePath = newImagePath;

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

            this.context.Update(cocktail);
            await this.context.SaveChangesAsync();
            var cocktailDto = this.dtoMapper.MapFrom(cocktail);

            return cocktailDto;
        }
    }
}
