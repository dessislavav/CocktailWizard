using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CocktailWizard.Data.DtoEntities;

namespace CocktailWizard.Services.Contracts
{
    public interface ICocktailCommentService
    {
        Task<CocktailCommentDto> CreateAsync(CocktailCommentDto tempCocktailComment);
        Task<ICollection<CocktailCommentDto>> GetCocktailCommentsAsync(Guid cocktailId);
        Task<CocktailCommentDto> DeleteAsync(Guid id, Guid cocktailId);
        Task<CocktailCommentDto> EditAsync(Guid id, string newBody);
    }
}