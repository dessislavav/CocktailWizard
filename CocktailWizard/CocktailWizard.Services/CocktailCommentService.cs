using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.ConstantMessages;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CocktailWizard.Services
{
    public class CocktailCommentService
    {
        private readonly CWContext context;
        private readonly IDtoMapper<CocktailComment, CocktailCommentDto> dtoMapper;

        public CocktailCommentService(CWContext context, IDtoMapper<CocktailComment, CocktailCommentDto> dtoMapper)
        {
            this.context = context;
            this.dtoMapper = dtoMapper;
        }

        public async Task<CocktailCommentDto> CreateAsync(CocktailCommentDto tempCocktailComment)
        {
            if (tempCocktailComment == null)
            {
                throw new BusinessLogicException(ExceptionMessages.CocktailCommentNull);
            }

            var user = await this.context.Users.FirstOrDefaultAsync(u => u.Id == tempCocktailComment.UserId);
            var userName = user.Email.Split('@')[0];

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
    }
}
