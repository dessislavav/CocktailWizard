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
    public class BarService
    {
        private readonly CWContext context;
        private readonly IDtoMapper<Bar, BarDto> dtoMapper;

        public BarService(CWContext context, IDtoMapper<Bar, BarDto> dtoMapper)
        {
            this.context = context;
            this.dtoMapper = dtoMapper;
        }

        public async Task<ICollection<BarDto>> GetAllBarsAsync()
        {
            var allBars = await this.context.Bars
                .Where(b => b.IsDeleted == false)
                .ToListAsync();

            var mappedBars = this.dtoMapper.MapFrom(allBars);

            return mappedBars;
        }

        public async Task<BarDto> GetBarAsync(Guid id)
        {

            var bar = await this.context.Bars
                .Where(b => b.IsDeleted == false)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bar == null)
            {
                throw new ArgumentException("//TODO");
            }

            var mappedBar = this.dtoMapper.MapFrom(bar);

            return mappedBar;
        }

        public async Task<BarDto> CreateAsync(BarDto tempBar)
        {
            if (tempBar == null)
            {
                throw new ArgumentException("//TODO");
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
                throw new ArgumentException("TODO");
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
                throw new ArgumentException("TODO");
            }
        }

        public async Task<BarDto> DeleteAsync(Guid id)
        {
            var bar = await this.context.Bars.FirstOrDefaultAsync(b => b.Id == id);

            if (bar == null)
            {
                throw new ArgumentException("TODO");
            }
            bar.IsDeleted = true;
            bar.DeletedOn = DateTime.UtcNow;
            await this.context.SaveChangesAsync();
            var barDto = this.dtoMapper.MapFrom(bar);

            return barDto;
        }
    }
}
