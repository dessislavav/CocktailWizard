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
using CocktailWizard.Services.Extensions;

namespace CocktailWizard.Services
{
    public class BarService
    {
        private readonly CWContext context;
        private readonly IDtoMapper<Bar, BarDto> dtoMapper;
        private readonly IDtoMapper<Bar, SearchBarDto> searchDtoMapper;

        public BarService(CWContext context, IDtoMapper<Bar, BarDto> dtoMapper, IDtoMapper<Bar, SearchBarDto> searchDtoMapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.dtoMapper = dtoMapper ?? throw new ArgumentNullException(nameof(dtoMapper));
            this.searchDtoMapper = searchDtoMapper ?? throw new ArgumentNullException(nameof(searchDtoMapper));
        }

        public async Task<BarDto> GetBarAsync(Guid id)
        {
            var bar = await this.context.Bars
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

        public async Task<ICollection<BarDto>> GetTopBarsAsync(int num)
        {
            var topBars = await this.context.Bars
                 .Where(b => b.IsDeleted == false)
                 .OrderBy(b => b.Name)
                 .Take(num)
                 .ToListAsync();

            if (!topBars.Any())
            {
                throw new BusinessLogicException(ExceptionMessages.BarNull);
            }

            var topBarsDtos = this.dtoMapper.MapFrom(topBars);

            return topBarsDtos;
        }

        public async Task<ICollection<BarDto>> GetTenBarsOrderedByNameAsync(int currentPage)
        {
            List<Bar> tenBars;
            if (currentPage == 1)
            {
                tenBars = await this.context.Bars
                    .Where(b => b.IsDeleted == false)
                    .OrderBy(x => x.Name)
                    .Take(10)
                    .ToListAsync();
            }
            else
            {
                tenBars = await this.context.Bars
                    .Where(b => b.IsDeleted == false)
                    .OrderBy(x => x.Name)
                    .Skip((currentPage - 1) * 10)
                    .Take(10)
                    .ToListAsync();
            }

            var dtoBars = this.dtoMapper.MapFrom(tenBars);

            return dtoBars;
        }

        public async Task<int> GetPageCountAsync(int barsPerPage)
        {
            var allBars = await context.Bars
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
                ImagePath = tempBar.ImagePath,
                Phone = tempBar.Phone
            };

            await this.context.Bars.AddAsync(bar);
            await this.context.SaveChangesAsync();

            var barDto = this.dtoMapper.MapFrom(bar);
            return barDto;
        }

        public async Task<BarDto> EditAsync(Guid id, string newName, string newInfo, string newAddress, string newPhone, string newImagePath)
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
                bar.ImagePath = newImagePath;

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
            }
            await this.context.SaveChangesAsync();

            return barDto;
        }

        public async Task<ICollection<SearchBarDto>> SearchAsync(string searchCriteria, bool byName, bool byAddress, bool byRating)
        {
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
            else
            {
                var allBars = this.context.Bars.Where(b => b.IsDeleted == false).Include(b => b.Ratings);
                var filteredByName = allBars.Where(b => byName && b.Name.Contains(terms));
                var filteredByAddress = allBars.Where(b => byAddress && b.Address.Contains(terms));
                //var filteredByRating = allBars.Where(b => byRating && b.Rating.Name.Contains(terms));

                var filtered = filteredByName.Union(filteredByAddress);
                // var filtered = filteredByName.Union(filteredByAddress).Union(filteredByRating);

                var mappedResult = filtered.Select(b => this.searchDtoMapper.MapFrom(b)).ToList();

                return mappedResult;
            }
        }
    }
}
