using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;

namespace CocktailWizard.Services.Contracts
{
    public interface IBanService
    {
        Task CheckForExpiredBansAsync();
        Task CreateAsync(Guid id, string description, int period);
        Task<ICollection<UserDto>> GetAllAsync(string param);
        Task RemoveAsync(Guid id);
    }
}