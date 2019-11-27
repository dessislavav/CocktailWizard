using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.ConstantMessages;
using CocktailWizard.Services.Contracts;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CocktailWizard.Services.Extensions;

namespace CocktailWizard.Services
{
    public class CocktailService : ICocktailService
    {
        private readonly CWContext context;
        private readonly IDtoMapper<Cocktail, CocktailDto> dtoMapper;
        private readonly IDtoMapper<Cocktail, DetailsCocktailDto> detailsCocktailDtoMapper;
        private readonly IDtoMapper<Bar, BarDto> barDtoMapper;
        private readonly IIngredientService ingredientService;
        private readonly ICocktailIngredientService cocktailIngredientService;

        public CocktailService(CWContext context,
            IDtoMapper<Cocktail, CocktailDto> dtoMapper,
            IDtoMapper<Bar, BarDto> barDtoMapper,
            IDtoMapper<Cocktail, DetailsCocktailDto> detailsCocktailDtoMapper,
            IIngredientService ingredientService,
            ICocktailIngredientService cocktailIngredientService)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.dtoMapper = dtoMapper ?? throw new ArgumentNullException(nameof(dtoMapper));
            this.barDtoMapper = barDtoMapper ?? throw new ArgumentNullException(nameof(barDtoMapper));
            this.detailsCocktailDtoMapper = detailsCocktailDtoMapper ?? throw new ArgumentNullException(nameof(barDtoMapper));
            this.ingredientService = ingredientService ?? throw new ArgumentNullException(nameof(ingredientService));
            this.cocktailIngredientService = cocktailIngredientService ?? throw new ArgumentNullException(nameof(cocktailIngredientService));
        }

        public async Task<ICollection<CocktailDto>> GetFiveCocktailsAsync(int currentPage, string sortOrder)
        {
            try
            {
                IQueryable<Cocktail> cocktails = this.context.Cocktails
                    .Include(c => c.Ratings)
                    .Where(c => c.IsDeleted == false);

                ICollection<Cocktail> fiveCocktails;

                switch (sortOrder)
                {
                    case "Name":
                        cocktails = cocktails.OrderBy(b => b.Name);
                        break;
                    case "name_desc":
                        cocktails = cocktails.OrderByDescending(b => b.Name);
                        break;
                    case "Rating":
                        cocktails = cocktails.OrderBy(b => b.Ratings.Count());
                        break;
                    case "rating_desc":
                        cocktails = cocktails.OrderByDescending(b => b.Ratings.Count());
                        break;
                    default:
                        cocktails = cocktails.OrderBy(b => b.Name);
                        break;
                }

                if (currentPage == 1)
                {
                    fiveCocktails = await cocktails
                        .Take(5)
                        .ToListAsync();
                }
                else
                {
                    fiveCocktails = await cocktails
                        .Skip((currentPage - 1) * 5)
                        .Take(5)
                        .ToListAsync();
                }

                var dtoCocktails = this.dtoMapper.MapFrom(fiveCocktails);

                return dtoCocktails;
            }
            catch (Exception)
            {

                throw new BusinessLogicException(ExceptionMessages.CocktailNull);
            }
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

        public async Task<DetailsCocktailDto> GetCocktailBarsAsync(Guid id)
        {
            var cocktail = await this.context.Cocktails
                .Include(c => c.Ratings)
                .Where(c => c.IsDeleted == false)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (cocktail == null)
            {
                throw new BusinessLogicException(ExceptionMessages.CocktailNull);
            }

            var detailsCocktailDto = this.detailsCocktailDtoMapper.MapFrom(cocktail);

            var bars = await this.context.BarCocktails
                .Where(b => b.IsDeleted == false)
                .Include(b => b.Bar)
                .Where(b => b.CocktailId == cocktail.Id)
                .Select(b => b.Bar)
                .ToListAsync();

            //if (!bars.Any())
            //{
            //    throw new BusinessLogicException(ExceptionMessages.BarCocktailNull);
            //}

            var mappedBars = this.barDtoMapper.MapFrom(bars);
            detailsCocktailDto.Bars = mappedBars;

            return detailsCocktailDto;
        }

        public async Task<ICollection<CocktailDto>> GetTopCocktailsAsync(int num)
        {
            var topCocktails = await this.context.Cocktails
                    .Include(c => c.Ratings)
                    .Where(b => b.IsDeleted == false)
                    .OrderByDescending(c => c.Ratings.Count())
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

            await this.context.Cocktails.AddAsync(cocktail);
            await this.context.SaveChangesAsync();

            foreach (var item in tempCocktail.CocktailIngredients)
            {
                var ingredient = await this.ingredientService.GetIngredientAsync(item) ?? throw new ArgumentException(ExceptionMessages.IngredientNull);

                var cocktailIngredient = await this.cocktailIngredientService.CreateCocktailIngredientAsync(cocktail.Id, ingredient.Id);
                cocktail.CocktailIngredients.Add(cocktailIngredient);
                await this.context.SaveChangesAsync();
            }

            var cocktailDto = this.dtoMapper.MapFrom(cocktail);
            return cocktailDto;
        }

        public async Task<CocktailDto> EditAsync(Guid id, string newName, string newInfo)
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

            var barCocktails = await this.context.BarCocktails
                .Where(b => b.CocktailId == cocktail.Id
                && b.IsDeleted == false)
                .ToListAsync();

            foreach (var item in barCocktails)
            {
                item.IsDeleted = true;
                item.DeletedOn = DateTime.UtcNow;
            }

            cocktail.IsDeleted = true;
            cocktail.DeletedOn = DateTime.UtcNow;

            this.context.Update(cocktail);
            await this.context.SaveChangesAsync();
            var cocktailDto = this.dtoMapper.MapFrom(cocktail);

            return cocktailDto;
        }

        public async Task<CocktailDto> AddBarsAsync(CocktailDto cocktailDto, List<string> selectedBars)
        {
            var cocktail = await this.context.Cocktails
                .Where(b => b.IsDeleted == false)
                .FirstOrDefaultAsync(b => b.Id == cocktailDto.Id);

            if (cocktail == null)
            {
                throw new BusinessLogicException(ExceptionMessages.CocktailNull);
            }

            if (!selectedBars.Any())
            {
                throw new BusinessLogicException(ExceptionMessages.BarNull);
            }

            foreach (var item in selectedBars)
            {
                var bar = await this.context.Bars
                    .Where(c => c.IsDeleted == false)
                    .FirstOrDefaultAsync(c => c.Name == item) ?? throw new BusinessLogicException(ExceptionMessages.BarNull);

                var barCocktail = await this.context.BarCocktails
                    .Where(c => c.BarId == bar.Id && c.CocktailId == cocktail.Id)
                    .FirstOrDefaultAsync();

                if (barCocktail == null)
                {
                    barCocktail = new BarCocktail
                    {
                        Bar = bar,
                        Cocktail = cocktail,
                    };
                    await this.context.BarCocktails.AddAsync(barCocktail);
                    bar.BarCocktails.Add(barCocktail);
                    cocktail.BarCocktails.Add(barCocktail);
                }
                else
                {
                    barCocktail.IsDeleted = false;
                    barCocktail.DeletedOn = DateTime.MinValue;
                }
            }

            await this.context.SaveChangesAsync();

            return cocktailDto;
        }

        public async Task<CocktailDto> RemoveBarsAsync(CocktailDto cocktailDto, List<string> selectedBars)
        {
            var cocktail = await this.context.Cocktails
                .Where(b => b.IsDeleted == false)
                .FirstOrDefaultAsync(b => b.Id == cocktailDto.Id);

            if (cocktail == null)
            {
                throw new BusinessLogicException(ExceptionMessages.BarNull);
            }

            if (!selectedBars.Any())
            {
                throw new BusinessLogicException(ExceptionMessages.CocktailNull);
            }

            foreach (var item in selectedBars)
            {
                var bar = await this.context.Bars
                    .Where(c => c.IsDeleted == false)
                    .FirstOrDefaultAsync(c => c.Name == item) ?? throw new BusinessLogicException(ExceptionMessages.BarNull);

                var barCocktail = await this.context.BarCocktails
                    .Where(c => c.IsDeleted == false)
                    .Where(c => c.CocktailId == cocktail.Id && c.BarId == bar.Id)
                    .FirstOrDefaultAsync();

                if (barCocktail == null)
                {
                    throw new BusinessLogicException(ExceptionMessages.BarNull);
                }

                barCocktail.IsDeleted = true;
                barCocktail.DeletedOn = DateTime.UtcNow;
            }
            await this.context.SaveChangesAsync();

            return cocktailDto;
        }


        public async Task<ICollection<CocktailDto>> SearchAsync(string searchCriteria, bool byName, bool byRating, double ratingValue)
        {
            var terms = searchCriteria.Split(" ");
            if (byName == false && byRating == false)
            {
                var resultDtos = await this.context.Cocktails
                    .Where(b => b.IsDeleted == false)
                    .Include(b => b.Ratings)
                    .Where(b => b.Name.Contains(terms))
                    .OrderBy(b => b.Name)
                    .Select(b => this.dtoMapper.MapFrom(b))
                    .ToListAsync();

                return resultDtos;
            }
            else
            {
                var allCocktails = this.context.Cocktails.Where(b => b.IsDeleted == false).Include(b => b.Ratings);
                var filteredByName = allCocktails.Where(b => byName && b.Name.Contains(terms));
                var filteredByRating = allCocktails.Where(b => byRating && b.Ratings.Any() ? (b.Ratings.Average(x => x.Value) >= ratingValue) : false);

                var filtered = filteredByName.Union(filteredByRating);

                var mappedResult = filtered.Select(b => this.dtoMapper.MapFrom(b)).ToList();

                return mappedResult;
            }
        }
    }
}
