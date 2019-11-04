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
    public class BarService
    {
        private readonly CWContext context;
        private readonly IDtoMapper<Bar, BarDto> dtoMapper;

        public BarService(CWContext context, IDtoMapper<Bar, BarDto> dtoMapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.dtoMapper = dtoMapper ?? throw new ArgumentNullException(nameof(dtoMapper));
        }

        public async Task<BarDto> GetBarAsync(Guid id)
        {

            var bar = await this.context.Bars
                .Where(b => b.IsDeleted == false)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bar == null)
            {
                throw new BusinessLogicException(ExceptionMessages.BarNull);
            }

            var mappedBar = this.dtoMapper.MapFrom(bar);

            return mappedBar;
        }

        public async Task<ICollection<BarDto>> GetAllBarsAsync()
        {
            var allBars = await this.context.Bars
                .Where(b => b.IsDeleted == false)
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

        public async Task<BarDto> EditAsync(BarDto barDto)
        {
            if (barDto == null)
            {
                throw new BusinessLogicException(ExceptionMessages.BarNull);
            }

            var bar = await this.context.Bars
                .Where(b => b.IsDeleted == false)
                .FirstOrDefaultAsync(b => b.Id == barDto.Id);

            try
            {
                bar.Name = barDto.Name;
                bar.ImagePath = barDto.ImagePath;
                bar.Address = barDto.Address;
                bar.Phone = barDto.Phone;

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

            if (!selectedCocktails.Any())
            {
                throw new ArgumentException("TODO");
            }
            foreach (var item in selectedCocktails)
            {
                var cocktail = await this.context.Cocktails
                    .FirstOrDefaultAsync(c => c.Name == item) ?? throw new ArgumentException("TODO");

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
    }
}
