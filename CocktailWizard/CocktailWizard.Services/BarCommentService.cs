using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.ConstantMessages;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Services
{
    public class BarCommentService
    {
        private readonly CWContext context;
        private readonly IDtoMapper<BarComment, BarCommentDto> dtoMapper;

        public BarCommentService(CWContext context, IDtoMapper<BarComment, BarCommentDto> dtoMapper)
        {
            this.context = context;
            this.dtoMapper = dtoMapper;
        }

        public async Task<BarCommentDto> CreateAsync(BarCommentDto tempBarComment)
        {
            if(tempBarComment == null)
            {
                throw new BusinessLogicException(ExceptionMessages.BarCommentNull);
            }

            var user = await this.context.Users.FirstOrDefaultAsync(u => u.Id == tempBarComment.UserId);
            var userName = user.Email.Split('@')[0];

            var barComment = new BarComment
            {
                BarId = tempBarComment.BarId,
                UserId = tempBarComment.UserId,
                Body = tempBarComment.Body,
                CreatedOn = tempBarComment.CreatedOn,
                ModifiedOn = tempBarComment.ModifiedOn,
                DeletedOn = tempBarComment.DeletedOn,
                IsDeleted = tempBarComment.IsDeleted
            };

            await this.context.BarComments.AddAsync(barComment);
            await this.context.SaveChangesAsync();

            var barCommentDto = this.dtoMapper.MapFrom(barComment);
            return barCommentDto;
        }

        public async Task<ICollection<BarCommentDto>> GetBarCommentsAsync(Guid barId)
        {
            var barComments = await this.context.BarComments
                .Include(bc => bc.Bar)
                .Include(bc => bc.User)
                .Where(bc => bc.IsDeleted == false)
                .Where(bc => bc.BarId == barId)
                .ToListAsync();

            var mappedBarComments = this.dtoMapper.MapFrom(barComments);
            return mappedBarComments;
                
        }
    }
}
