using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Web.Areas.Manager.Models;
using CocktailWizard.Web.Mappers.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace CocktailWizard.Web.Mappers
{
    public class UserViewModelMapper : IViewModelMapper<UserDto, UserViewModel>
    {
        public UserViewModel MapFrom(UserDto dtoEntity)
        {
            if (dtoEntity == null)
            {
                return null;
            }

            return new UserViewModel
            {
                Id = dtoEntity.Id,
                UserName = dtoEntity.UserName,
                Email = dtoEntity.Email,
                CreatedOn = dtoEntity.CreatedOn,
                IsBanned = dtoEntity.IsBanned,
                ReasonForBan = dtoEntity.ReasonForBan
            };
        }

        public ICollection<UserViewModel> MapFrom(ICollection<UserDto> dtoEntities)
        {
            return dtoEntities.Select(this.MapFrom).ToList();
        }

        public UserDto MapFrom(UserViewModel entityVM)
        {
            if (entityVM == null)
            {
                return null;
            }

            return new UserDto
            {
                Id = entityVM.Id,
                UserName = entityVM.UserName,
                Email = entityVM.Email,
                CreatedOn = entityVM.CreatedOn,
                IsBanned = entityVM.IsBanned,
                ReasonForBan = entityVM.ReasonForBan
            };
        }

        public ICollection<UserDto> MapFrom(ICollection<UserViewModel> entitiesVM)
        {
            return entitiesVM.Select(this.MapFrom).ToList();
        }
    }
}
