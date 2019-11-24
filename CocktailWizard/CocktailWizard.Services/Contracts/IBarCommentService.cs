using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;

namespace CocktailWizard.Services.Contracts
{
    public interface IBarCommentService
    {
        Task<BarCommentDto> CreateAsync(BarCommentDto tempBarComment);
        Task<BarCommentDto> DeleteAsync(Guid id, Guid barId);
        //Task<BarComment> GetBarCommentAsync(Guid barId);
        Task<ICollection<BarCommentDto>> GetBarCommentsAsync(Guid barId);
        Task<BarCommentDto> EditAsync(Guid id, string newBody);
    }
}