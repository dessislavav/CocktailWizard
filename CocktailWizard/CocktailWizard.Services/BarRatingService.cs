using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.ConstantMessages;
using CocktailWizard.Services.Contracts;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Services
{
    public class BarRatingService : IBarRatingService
    {
        private readonly CWContext context;
        private readonly IDtoMapper<BarRating, BarRatingDto> dtoMapper;

        public BarRatingService(CWContext context,
            IDtoMapper<BarRating, BarRatingDto> dtoMapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.dtoMapper = dtoMapper ?? throw new ArgumentNullException(nameof(dtoMapper));
        }

        public async Task<BarRatingDto> CreateAsync(BarRatingDto tempBarRating)
        {
            if (tempBarRating == null)
            {
                throw new BusinessLogicException(ExceptionMessages.BarRatingNull);
            }

            var barRating = new BarRating
            {
                Value = tempBarRating.Value,
                UserId = tempBarRating.UserId,
                BarId = tempBarRating.BarId,
                CreatedOn = tempBarRating.CreatedOn,
                ModifiedOn = tempBarRating.ModifiedOn,
                DeletedOn = tempBarRating.DeletedOn,
                IsDeleted = tempBarRating.IsDeleted

            };

            await this.context.BarRatings.AddAsync(barRating);
            await this.context.SaveChangesAsync();

            var barRatingDto = this.dtoMapper.MapFrom(barRating);

            return barRatingDto;
        }

        public async Task<ICollection<BarRatingDto>> GetAllRatingsAsync(Guid barId)
        {
            var barRatings = await this.context.BarRatings
                .Include(br => br.Bar)
                .Include(br => br.User)
                .Where(br => br.IsDeleted == false)
                .Where(br => br.BarId == barId)
                .ToListAsync();

            var barRatingDtos = this.dtoMapper.MapFrom(barRatings);

            return barRatingDtos;
        }

        public async Task<BarRatingDto> GetRatingAsync(Guid barId, Guid userId)
        {
            var barRating = await this.context.BarRatings
                .Include(br => br.Bar)
                .Include(br => br.User)
                .Where(br => br.IsDeleted == false)
                .Where(br => br.BarId == barId 
                 && br.UserId == userId)
                .FirstOrDefaultAsync();

            if (barRating == null)
            {
                throw new BusinessLogicException(ExceptionMessages.BarRatingNull);
            }
            var barRatingDto = this.dtoMapper.MapFrom(barRating);

            return barRatingDto;
        }
    }
}
