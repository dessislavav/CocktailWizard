using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CocktailWizard.Data.DtoEntities;

namespace CocktailWizard.Services.Contracts
{
    public interface ICocktailService
    {
        Task<CocktailDto> CreateAsync(CocktailDto tempCocktail);
        Task<CocktailDto> DeleteAsync(Guid id);
        Task<CocktailDto> EditAsync(Guid id, string newName, string newInfo, string newImagePath);
        Task<ICollection<CocktailDto>> GetAllCocktailsAsync();
        Task<CocktailDto> GetCocktailAsync(Guid id);
        Task<DetailsCocktailDto> GetCocktailsBars(Guid id);
        Task<int> GetPageCountAsync(int cocktailsPerPage);
        Task<ICollection<CocktailDto>> GetTenCocktailsOrderedByNameAsync(int currentPage);
        Task<ICollection<CocktailDto>> GetTopCocktails(int num);
        Task<ICollection<CocktailDto>> SearchAsync(string searchCriteria, bool byName, bool byRating, double ratingValue);
    }
}