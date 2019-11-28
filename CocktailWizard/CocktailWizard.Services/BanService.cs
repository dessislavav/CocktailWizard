using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.ConstantMessages;
using CocktailWizard.Services.Contracts;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Services
{
    public class BanService : IBanService
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
                    .Where(u => u.IsBanned == false
                    && u.UserName != "manager@cw.com")
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

        public async Task<UserDto> GetBannedUserAsync(Guid id)
        {
            var user = await this.context.Users
                .Include(u => u.Bans)
                .Where(u => u.IsBanned == true)
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();

            var mappedUser = this.dtoMapper.MapFrom(user);
            return mappedUser;
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

        public async Task RemoveAsync(Guid id)
        {

            var ban = await this.context.Bans
            .Include(u => u.User)
            .Where(b => b.User.Id == id
            && b.HasExpired == false)
            .FirstOrDefaultAsync();

            if (ban == null)
            {
                throw new BusinessLogicException(ExceptionMessages.ModelError);
            }

            ban.ExpiresOn = DateTime.UtcNow;
            ban.HasExpired = true;
            ban.User.IsBanned = false;
            ban.User.LockoutEnd = DateTime.UtcNow;
            ban.User.LockoutEnabled = false;

            await this.context.SaveChangesAsync();
        }

        public async Task CheckForExpiredBansAsync()
        {
            var expiredBans = await this.context.Bans
                .Include(b => b.User)
                .Where(b => b.ExpiresOn < DateTime.UtcNow)
                .ToListAsync();

            expiredBans.ForEach(b => b.HasExpired = true);
            expiredBans.ForEach(b => b.User.IsBanned = false);
            expiredBans.ForEach(b => b.User.LockoutEnabled = false);

            return;
        }
    }
}
