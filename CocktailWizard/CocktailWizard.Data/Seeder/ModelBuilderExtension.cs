using CocktailWizard.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailWizard.Data.Seeder
{
    public static class ModelBuilderExtension
    {
        public static void Seeder(this ModelBuilder builder)
        {
            builder.Entity<Role>().HasData(
                new Role { Id = Guid.Parse("297D06E6-C058-486F-A18A-06A971EBFCD7"), Name = "Admin", NormalizedName = "ADMIN" },
                new Role { Id = Guid.Parse("6C8FCD7E-62F6-4F3E-A73D-ACBFD60B97AB"), Name = "Member", NormalizedName = "MEMBER" }
            );

            var hasher = new PasswordHasher<User>();

            User adminUser = new User { Id = Guid.Parse("7BD06FE6-79CA-43A1-862B-446A1466BB93"), UserName = "admin@cw.com", NormalizedUserName = "ADMIN@CW.COM", Email = "admin@cw.com", NormalizedEmail = "ADMIN@CW.COM", CreatedOn = DateTime.UtcNow, LockoutEnabled = true, SecurityStamp = "7I5VNHIJTSZNOT3KDWKNFUV5PVYBHGXN" };
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "admin");

            builder.Entity<User>().HasData(adminUser);

            builder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid>
                {
                    RoleId = Guid.Parse("297D06E6-C058-486F-A18A-06A971EBFCD7"),
                    UserId = adminUser.Id
                });
        }
    }
}
