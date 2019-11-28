using CocktailWizard.Services.DtoEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CocktailWizard.Services.Contracts
{
    public interface IBanService
    {
        Task CheckForExpiredBansAsync();
        Task CreateAsync(Guid id, string description, int period);
        Task<ICollection<UserDto>> GetAllAsync(string param);
        Task<UserDto> GetBannedUserAsync(Guid id);
        Task RemoveAsync(Guid id);
    }
}