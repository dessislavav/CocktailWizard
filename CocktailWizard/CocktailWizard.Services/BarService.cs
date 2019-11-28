using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.ConstantMessages;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Services.DtoMappers.Contracts;
using CocktailWizard.Services.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Services
{
    public class BarService : IBarService
    {
        private readonly CWContext context;
        private readonly IDtoMapper<Bar, BarDto> dtoMapper;
        private readonly IDtoMapper<Bar, SearchBarDto> searchDtoMapper;
        private readonly IDtoMapper<Cocktail, CocktailDto> cocktailDtoMapper;

        public BarService(CWContext context,
                          IDtoMapper<Bar, BarDto> dtoMapper,
                          IDtoMapper<Bar, SearchBarDto> searchDtoMapper,
                          IDtoMapper<Cocktail, CocktailDto> cocktailDtoMapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.dtoMapper = dtoMapper ?? throw new ArgumentNullException(nameof(dtoMapper));
            this.searchDtoMapper = searchDtoMapper ?? throw new ArgumentNullException(nameof(searchDtoMapper));
            this.cocktailDtoMapper = cocktailDtoMapper ?? throw new ArgumentNullException(nameof(cocktailDtoMapper));
        }

        public async Task<BarDto> GetBarAsync(Guid id)
        {
            var bar = await this.context.Bars
                .Include(b => b.BarCocktails)
                .Where(b => b.IsDeleted == false)
                .OrderBy(b => b.Name)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bar == null)
            {
                throw new BusinessLogicException(ExceptionMessages.BarNull);
            }

            var mappedBar = this.dtoMapper.MapFrom(bar);

            return mappedBar;
        }

        public async Task<BarDto> GetBarCocktailsAsync(Guid id)
        {
            var bar = await this.context.Bars
                .Include(b => b.Ratings)
                .Where(b => b.IsDeleted == false)
                .Include(b => b.Ratings)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bar == null)
            {
                throw new BusinessLogicException(ExceptionMessages.BarNull);
            }

            var barDto = this.dtoMapper.MapFrom(bar);
            var cocktails = await this.context.BarCocktails
                .Where(b => b.IsDeleted == false)
                .Include(b => b.Cocktail)
                .Where(b => b.BarId == bar.Id)
                .Select(b => b.Cocktail)
                .ToListAsync();

            var mappedCocktails = this.cocktailDtoMapper.MapFrom(cocktails);
            barDto.Cocktails = mappedCocktails;

            return barDto;

        }

        public async Task<ICollection<BarDto>> GetTopBarsAsync(int num)
        {
            var topBars = await this.context.Bars
                 .Include(b => b.Ratings)
                 .Where(b => b.IsDeleted == false)
                 .OrderByDescending(b => b.Ratings.Count())
                 .Take(num)
                 .ToListAsync();

            if (!topBars.Any())
            {
                throw new BusinessLogicException(ExceptionMessages.BarNull);
            }

            var topBarsDtos = this.dtoMapper.MapFrom(topBars);

            return topBarsDtos;
        }

        public async Task<ICollection<BarDto>> GetFiveBarsAsync(int currentPage, string sortOrder)
        {
            try
            {
                IQueryable<Bar> bars = this.context.Bars
                    .Include(b => b.Ratings)
                    .Where(b => b.IsDeleted == false);

                ICollection<Bar> fiveBars;

                switch (sortOrder)
                {
                    case "Name":
                        bars = bars.OrderBy(b => b.Name);
                        break;
                    case "name_desc":
                        bars = bars.OrderByDescending(b => b.Name);
                        break;
                    case "Rating":
                        bars = bars.OrderBy(b => b.Ratings.Count());
                        break;
                    case "rating_desc":
                        bars = bars.OrderByDescending(b => b.Ratings.Count());
                        break;
                    default:
                        bars = bars.OrderBy(b => b.Name);
                        break;
                }

                if (currentPage == 1)
                {
                    fiveBars = await bars
                        .Take(5)
                        .ToListAsync();
                }
                else
                {
                    fiveBars = await bars
                        .Skip((currentPage - 1) * 5)
                        .Take(5)
                        .ToListAsync();
                }

                var dtoBars = this.dtoMapper.MapFrom(fiveBars);

                return dtoBars;
            }
            catch (Exception)
            {
                throw new BusinessLogicException(ExceptionMessages.BarNull);
            }
        }

        public async Task<int> GetPageCountAsync(int barsPerPage)
        {
            var allBars = await context.Bars
                .Where(b => b.IsDeleted == false)
                .CountAsync();

            var totalPages = (allBars - 1) / barsPerPage + 1;

            return totalPages;
        }

        public async Task<ICollection<BarDto>> GetAllBarsAsync()
        {
            var allBars = await this.context.Bars
                .Where(b => b.IsDeleted == false)
                .OrderBy(b => b.Name)
                .ToListAsync();

            if (!allBars.Any())
            {
                throw new BusinessLogicException(ExceptionMessages.BarNull);
            }

            var mappedBars = this.dtoMapper.MapFrom(allBars);

            return mappedBars;
        }



        public async Task<BarDto> CreateAsync(BarDto tempBar)
        {
            if (tempBar == null)
            {
                throw new BusinessLogicException(ExceptionMessages.BarNull);
            }

            var bar = new Bar
            {
                Name = tempBar.Name,
                Info = tempBar.Info,
                Address = tempBar.Address,
                GoogleMapsURL = tempBar.GoogleMapsURL,
                ImagePath = tempBar.ImagePath,
                Phone = tempBar.Phone
            };

            await this.context.Bars.AddAsync(bar);
            await this.context.SaveChangesAsync();

            var barDto = this.dtoMapper.MapFrom(bar);
            return barDto;
        }

        public async Task<BarDto> EditAsync(Guid id, string newName, string newInfo, string newAddress, string newPhone)
        {
            var bar = await this.context.Bars
                .Where(b => b.IsDeleted == false)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bar == null)
            {
                throw new BusinessLogicException(ExceptionMessages.BarNull);
            }

            try
            {
                bar.Name = newName;
                bar.Info = newInfo;
                bar.Address = newAddress;
                bar.Phone = newPhone;

                this.context.Update(bar);
                await this.context.SaveChangesAsync();
                var editedBarDto = this.dtoMapper.MapFrom(bar);

                return editedBarDto;
            }
            catch (Exception)
            {
                throw new BusinessLogicException(ExceptionMessages.GeneralOopsMessage);
            }
        }

        public async Task<BarDto> DeleteAsync(Guid id)
        {
            var bar = await this.context.Bars.FirstOrDefaultAsync(b => b.Id == id);

            if (bar == null)
            {
                throw new BusinessLogicException(ExceptionMessages.BarNull);
            }

            var barCocktails = await this.context.BarCocktails
                .Where(b => b.BarId == bar.Id
                && b.IsDeleted == false)
                .ToListAsync();

            foreach (var item in barCocktails)
            {
                item.IsDeleted = true;
                item.DeletedOn = DateTime.UtcNow;
            }

            bar.IsDeleted = true;
            bar.DeletedOn = DateTime.UtcNow;
            await this.context.SaveChangesAsync();
            var barDto = this.dtoMapper.MapFrom(bar);

            return barDto;
        }

        public async Task<BarDto> AddCocktailsAsync(BarDto barDto, List<string> selectedCocktails)
        {
            var bar = await this.context.Bars
                .Where(b => b.IsDeleted == false)
                .FirstOrDefaultAsync(b => b.Id == barDto.Id);

            if (bar == null)
            {
                throw new BusinessLogicException(ExceptionMessages.BarNull);
            }

            if (!selectedCocktails.Any())
            {
                throw new BusinessLogicException(ExceptionMessages.CocktailNull);
            }

            foreach (var item in selectedCocktails)
            {
                var cocktail = await this.context.Cocktails
                    .Where(c => c.IsDeleted == false)
                    .FirstOrDefaultAsync(c => c.Name == item) ?? throw new BusinessLogicException(ExceptionMessages.CocktailNull);

                var barCocktail = await this.context.BarCocktails
                    .Where(c => c.CocktailId == cocktail.Id && c.BarId == bar.Id)
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

            return barDto;
        }

        public async Task<BarDto> RemoveCocktailsAsync(BarDto barDto, List<string> selectedCocktails)
        {
            var bar = await this.context.Bars
                .Where(b => b.IsDeleted == false)
                .FirstOrDefaultAsync(b => b.Id == barDto.Id);

            if (bar == null)
            {
                throw new BusinessLogicException(ExceptionMessages.BarNull);
            }

            if (!selectedCocktails.Any())
            {
                throw new BusinessLogicException(ExceptionMessages.CocktailNull);
            }

            foreach (var item in selectedCocktails)
            {
                var cocktail = await this.context.Cocktails
                    .Where(c => c.IsDeleted == false)
                    .FirstOrDefaultAsync(c => c.Name == item) ?? throw new BusinessLogicException(ExceptionMessages.CocktailNull);

                var barCocktail = await this.context.BarCocktails
                    .Where(c => c.IsDeleted == false)
                    .Where(c => c.CocktailId == cocktail.Id && c.BarId == bar.Id)
                    .FirstOrDefaultAsync();

                if (barCocktail == null)
                {
                    throw new BusinessLogicException(ExceptionMessages.CocktailNull);
                }

                barCocktail.IsDeleted = true;
                barCocktail.DeletedOn = DateTime.UtcNow;
            }
            await this.context.SaveChangesAsync();

            return barDto;
        }

        public async Task<ICollection<SearchBarDto>> SearchAsync(string searchCriteria, bool byName, bool byAddress, bool byRating, double ratingValue)
        {
            //Case where only rating is selected as a search criteria
            if (string.IsNullOrEmpty(searchCriteria))
            {
                var allBars = this.context.Bars.Where(b => b.IsDeleted == false).Include(b => b.Ratings);
                var filteredByRating = await allBars.Where(b => byRating && b.Ratings.Any() ? (b.Ratings.Average(x => x.Value) >= ratingValue) : false).ToListAsync();
                var mappedResult = this.searchDtoMapper.MapFrom(filteredByRating);

                return mappedResult;
            }

            //Case where no criterias are selected so all bars are filtered
            var terms = searchCriteria.Split(" ");
            if (byName == false && byAddress == false && byRating == false)
            {
                var resultDtos = await this.context.Bars
                    .Where(b => b.IsDeleted == false)
                    .Include(b => b.Ratings)
                    .Where(b => b.Name.Contains(terms)
                    || b.Address.Contains(terms))
                    .OrderBy(b => b.Name)
                    .Select(b => this.searchDtoMapper.MapFrom(b))
                    .ToListAsync();

                return resultDtos;
            }

            //Case where certain criterias are selected so we filter only those bars
            else
            {
                var allBars = this.context.Bars.Where(b => b.IsDeleted == false).Include(b => b.Ratings);
                var filteredByName = allBars.Where(b => byName && b.Name.Contains(terms));
                var filteredByAddress = allBars.Where(b => byAddress && b.Address.Contains(terms));
                var filteredByRating = allBars.Where(b => byRating && b.Ratings.Any() ? (b.Ratings.Average(x => x.Value) >= ratingValue) : false);
                var filtered = filteredByName.Union(filteredByAddress).Union(filteredByRating);
                var mappedResult = filtered.Select(b => this.searchDtoMapper.MapFrom(b)).ToList();

                return mappedResult;
            }
        }
    }
}
