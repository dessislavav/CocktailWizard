﻿using CocktailWizard.Services.DtoEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CocktailWizard.Services
{
    public interface IBarService
    {
        Task<BarDto> AddCocktailsAsync(BarDto barDto, List<string> selectedCocktails);
        Task<BarDto> RemoveCocktailsAsync(BarDto barDto, List<string> selectedCocktails);
        Task<BarDto> CreateAsync(BarDto tempBar);
        Task<BarDto> DeleteAsync(Guid id);
        Task<BarDto> EditAsync(Guid id, string newName, string newInfo, string newAddress, string newPhone);
        Task<ICollection<BarDto>> GetAllBarsAsync();
        Task<BarDto> GetBarAsync(Guid id);
        Task<BarDto> GetBarCocktailsAsync(Guid id);
        Task<int> GetPageCountAsync(int barsPerPage);
        Task<ICollection<BarDto>> GetFiveBarsAsync(int currentPage, string sortOrder);
        Task<ICollection<BarDto>> GetTopBarsAsync(int num);
        Task<ICollection<SearchBarDto>> SearchAsync(string searchCriteria, bool byName, bool byAddress, bool byRating, double ratingValue);
    }
}