﻿using CocktailWizard.Services.DtoEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Contracts
{
    public interface ICocktailService
    {
        Task<CocktailDto> CreateAsync(CocktailDto tempCocktail);
        Task<CocktailDto> DeleteAsync(Guid id);
        Task<CocktailDto> EditAsync(Guid id, string newName, string newInfo);
        Task<ICollection<CocktailDto>> GetAllCocktailsAsync();
        Task<CocktailDto> GetCocktailAsync(Guid id);
        Task<DetailsCocktailDto> GetCocktailBarsAsync(Guid id);
        Task<int> GetPageCountAsync(int cocktailsPerPage);
        Task<ICollection<CocktailDto>> GetFiveCocktailsAsync(int currentPage, string sortOrder);
        Task<ICollection<CocktailDto>> GetTopCocktailsAsync(int num);
        Task<CocktailDto> AddBarsAsync(CocktailDto cocktailDto, List<string> selectedBars);
        Task<CocktailDto> RemoveBarsAsync(CocktailDto cocktailDto, List<string> selectedBars);
        Task<ICollection<CocktailDto>> SearchAsync(string searchCriteria, bool byName, bool byRating, double ratingValue);
    }
}