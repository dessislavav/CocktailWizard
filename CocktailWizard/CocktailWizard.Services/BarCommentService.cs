using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.ConstantMessages;
using CocktailWizard.Services.Contracts;
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
    public class BarCommentService : IBarCommentService
    {
        private readonly CWContext context;
        private readonly IDtoMapper<BarComment, BarCommentDto> dtoMapper;

        public BarCommentService(CWContext context, IDtoMapper<BarComment, BarCommentDto> dtoMapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.dtoMapper = dtoMapper ?? throw new ArgumentNullException(nameof(dtoMapper));
        }

        public async Task<BarCommentDto> CreateAsync(BarCommentDto tempBarComment)
        {
            if (tempBarComment == null)
            {
                throw new BusinessLogicException(ExceptionMessages.BarCommentNull);
            }

            var barComment = new BarComment
            {
                Id = tempBarComment.Id,
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

            var barCommentDtos = this.dtoMapper.MapFrom(barComments);

            return barCommentDtos;

        }

        // Returns BarComment!!!
        public async Task<BarComment> GetBarCommentAsync(Guid barId)
        {
            var barComment = await this.context.BarComments
                .Include(bc => bc.Bar)
                .Include(bc => bc.User)
                .Where(bc => bc.IsDeleted == false)
                .Where(bc => bc.BarId == barId)
                .FirstOrDefaultAsync();

            //var barCommentDto = this.dtoMapper.MapFrom(barComment);

            return barComment;
        }

        public async Task<BarCommentDto> DeleteAsync(Guid barId)
        {
            var comment = await this.GetBarCommentAsync(barId);

            comment.DeletedOn = DateTime.UtcNow;
            comment.IsDeleted = true;

            this.context.Update(comment);
            await this.context.SaveChangesAsync();

            var commentDto = this.dtoMapper.MapFrom(comment);

            return commentDto;
        }

        public async Task<BarCommentDto> EditAsync(Guid id, string newBody)
        {
            var comment = await this.context.BarComments
                .Where(bc => bc.IsDeleted == false)
                .FirstOrDefaultAsync(bc => bc.Id == id);

            comment.ModifiedOn = DateTime.UtcNow;
            comment.Body = newBody;

            this.context.Update(comment);
            await this.context.SaveChangesAsync();

            var commentDto = this.dtoMapper.MapFrom(comment);

            return commentDto;
        }
    }
}
