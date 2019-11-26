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
using System.Text;
using System.Threading.Tasks;

namespace CocktailWizard.Services
{
    public class CocktailRatingService : ICocktailRatingService
    {
        private readonly CWContext context;
        private readonly IDtoMapper<CocktailRating, CocktailRatingDto> dtoMapper;

        public CocktailRatingService(CWContext context,
            IDtoMapper<CocktailRating, CocktailRatingDto> dtoMapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.dtoMapper = dtoMapper ?? throw new ArgumentNullException(nameof(dtoMapper));
        }

        public async Task<CocktailRatingDto> CreateAsync(CocktailRatingDto tempCocktailRating)
        {
            if (tempCocktailRating == null)
            {
                throw new BusinessLogicException(ExceptionMessages.CocktailRatingNull);
            }

            var cocktailRating = new CocktailRating
            {
                Value = tempCocktailRating.Value,
                UserId = tempCocktailRating.UserId,
                CocktailId = tempCocktailRating.CocktailId,
                CreatedOn = tempCocktailRating.CreatedOn,
                ModifiedOn = tempCocktailRating.ModifiedOn,
                DeletedOn = tempCocktailRating.DeletedOn,
                IsDeleted = tempCocktailRating.IsDeleted
            };

            await this.context.CocktailRatings.AddAsync(cocktailRating);
            await this.context.SaveChangesAsync();

            var cocktailRatingDto = this.dtoMapper.MapFrom(cocktailRating);

            return cocktailRatingDto;
        }

        public async Task<ICollection<CocktailRatingDto>> GetAllRatingsAsync(Guid cocktailId)
        {
            var cocktailRatings = await this.context.CocktailRatings
                .Include(cc => cc.Cocktail)
                .Include(cc => cc.User)
                .Where(cc => cc.IsDeleted == false)
                .Where(cc => cc.CocktailId == cocktailId)
                .ToListAsync();

            var cocktailRatingDtos = this.dtoMapper.MapFrom(cocktailRatings);

            return cocktailRatingDtos;
        }

        public async Task<CocktailRatingDto> GetRatingAsync(Guid cocktailId, Guid userId)
        {
            var cocktailRating = await this.context.CocktailRatings
                .Include(br => br.Cocktail)
                .Include(br => br.User)
                .Where(br => br.IsDeleted == false)
                .Where(br => br.CocktailId == cocktailId
                 && br.UserId == userId)
                .FirstOrDefaultAsync();

            if (cocktailRating == null)
            {
                throw new BusinessLogicException(ExceptionMessages.CocktailRatingNull);
            }
            var cocktailRatingDto = this.dtoMapper.MapFrom(cocktailRating);

            return cocktailRatingDto;
        }

    }
}
