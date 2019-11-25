using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.ConstantMessages;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoMappers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CocktailWizard.Services.DtoMappers
{
    public class UserDtoMapper : IDtoMapper<User, UserDto>
    {
        public UserDto MapFrom(User entity)
        {
            if (entity == null)
            {
                throw new BusinessLogicException(ExceptionMessages.EntityNull);
            }

            return new UserDto
            {
                Id = entity.Id,
                UserName = entity.UserName,
                Email = entity.Email,
                CreatedOn = entity.CreatedOn,
                IsBanned = entity.IsBanned,
                ReasonForBan = entity.Bans
                      .Where(b => b.HasExpired == false)
                      .Select(b => b.Description)
                      .FirstOrDefault()
            };
        }

        public ICollection<UserDto> MapFrom(ICollection<User> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }
    }
}
