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
    public class CocktailCommentService : ICocktailCommentService
    {
        private readonly CWContext context;
        private readonly IDtoMapper<CocktailComment, CocktailCommentDto> dtoMapper;

        public CocktailCommentService(CWContext context, IDtoMapper<CocktailComment, CocktailCommentDto> dtoMapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.dtoMapper = dtoMapper ?? throw new ArgumentNullException(nameof(dtoMapper));
        }

        public async Task<CocktailCommentDto> CreateAsync(CocktailCommentDto tempCocktailComment)
        {
            if (tempCocktailComment == null)
            {
                throw new BusinessLogicException(ExceptionMessages.CocktailCommentNull);
            }

            var cocktailComment = new CocktailComment
            {
                Id = tempCocktailComment.Id,
                CocktailId = tempCocktailComment.CocktailId,
                UserId = tempCocktailComment.UserId,
                Body = tempCocktailComment.Body,
                CreatedOn = tempCocktailComment.CreatedOn,
                ModifiedOn = tempCocktailComment.ModifiedOn,
                DeletedOn = tempCocktailComment.DeletedOn,
                IsDeleted = tempCocktailComment.IsDeleted
            };

            await this.context.CocktailComments.AddAsync(cocktailComment);
            await this.context.SaveChangesAsync();

            var cocktailCommentDto = this.dtoMapper.MapFrom(cocktailComment);
            return cocktailCommentDto;
        }

        public async Task<ICollection<CocktailCommentDto>> GetCocktailCommentsAsync(Guid cocktailId)
        {
            var cocktailComments = await this.context.CocktailComments
                .Include(cc => cc.Cocktail)
                .Include(cc => cc.User)
                .Where(cc => cc.IsDeleted == false)
                .Where(cc => cc.CocktailId == cocktailId)
                .ToListAsync();

            var cocktailCommentDtos = this.dtoMapper.MapFrom(cocktailComments);
            return cocktailCommentDtos;

        }

        public async Task<CocktailCommentDto> DeleteAsync(Guid id, Guid cocktailId)
        {
            var comment = await this.context.CocktailComments
                .Include(bc => bc.Cocktail)
                .Include(bc => bc.User)
                .Where(bc => bc.IsDeleted == false)
                .Where(bc => bc.Id == id)
                .Where(bc => bc.CocktailId== cocktailId)
                .FirstOrDefaultAsync();

            if (comment == null)
            {
                throw new BusinessLogicException(ExceptionMessages.CocktailCommentNull);
            }

            comment.DeletedOn = DateTime.UtcNow;
            comment.IsDeleted = true;

            this.context.Update(comment);
            await this.context.SaveChangesAsync();

            var commentDto = this.dtoMapper.MapFrom(comment);

            return commentDto;
        }

        public async Task<CocktailCommentDto> EditAsync(Guid id, string newBody)
        {
            var comment = await this.context.CocktailComments
                .Where(bc => bc.IsDeleted == false)
                .FirstOrDefaultAsync(bc => bc.Id == id);

            if (comment == null)
            {
                throw new BusinessLogicException(ExceptionMessages.CocktailCommentNull);
            }

            comment.ModifiedOn = DateTime.UtcNow;
            comment.Body = newBody;

            this.context.Update(comment);
            await this.context.SaveChangesAsync();

            var commentDto = this.dtoMapper.MapFrom(comment);

            return commentDto;
        }
    }
}
