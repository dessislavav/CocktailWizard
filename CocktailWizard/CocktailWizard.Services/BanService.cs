using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.ConstantMessages;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoMappers;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailWizard.Services
{
    public class BanService
    {
        private readonly CWContext context;
        private readonly IDtoMapper<User, UserDto> dtoMapper;

        public BanService(CWContext context, IDtoMapper<User, UserDto> dtoMapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.dtoMapper = dtoMapper ?? throw new ArgumentNullException(nameof(dtoMapper));
        }

        public async Task<ICollection<UserDto>> GetAllAsync(string param)
        {
            var users = new List<User>();
            if (param == "active")
            {
                users = await this.context.Users
                    .Where(u => u.IsBanned == false)
                    .ToListAsync();
            }
            else if (param == "banned")
            {
                users = await this.context.Users
                    .Include(b => b.Bans)
                    .Where(u => u.IsBanned == true)
                    .ToListAsync();
            }

            var mappedUsers = this.dtoMapper.MapFrom(users);
            return mappedUsers;
        }

        public async Task CreateAsync(Guid id, string description, int period)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(b => b.Id == id);

            if (user == null)
            {
                throw new BusinessLogicException(ExceptionMessages.ModelError);
            }

            try
            {
                var ban = new Ban()
                {
                    Description = description,
                    User = user,
                    ExpiresOn = DateTime.UtcNow.AddDays(period)
                };

                user.Bans.Add(ban);
                user.IsBanned = true;
                user.LockoutEnabled = true;
                user.LockoutEnd = ban.ExpiresOn;
                await this.context.Bans.AddAsync(ban);
                await this.context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new BusinessLogicException(ExceptionMessages.ModelError);
            }
        }

        public async Task RemoveAsync(User user)
        {
            if (user != null)
            {
                var ban = await this.context.Bans
                .Include(u => u.User)
                .Where(b => b.User == user
                && b.HasExpired == false)
                .FirstOrDefaultAsync();

                ban.ExpiresOn = DateTime.UtcNow;
                ban.HasExpired = true;
                user.IsBanned = false;
                user.LockoutEnd = DateTime.UtcNow;
                user.LockoutEnabled = false;

                await this.context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentNullException("User value null");
            }

        }

        public async Task CheckForExpiredBansAsync()
        {
            //var expiredBans = await this.context.Bans.Include(b => b.User).Where(b => b. < DateTime.Now).ToListAsync();

            //expiredBans.ForEach(b => b.Expired = true);
            //expiredBans.ForEach(b => b.User.IsBanned = false);
            //expiredBans.ForEach(b => b.User.LockoutEnabled = false);

            //return;
        }
    }
}
