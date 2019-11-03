using CocktailWizard.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace CocktailWizard.Data.Seeder
{
    public static class ModelBuilderExtension
    {
        public static void Seeder(this ModelBuilder builder)
        {
            //SEEDING ROLES
            builder.Entity<Role>().HasData(
                new Role { Id = Guid.Parse("297D06E6-C058-486F-A18A-06A971EBFCD7"), Name = "Manager", NormalizedName = "MANAGER" },
                new Role { Id = Guid.Parse("6C8FCD7E-62F6-4F3E-A73D-ACBFD60B97AB"), Name = "Member", NormalizedName = "MEMBER" }
            );

            //SEEDING MANAGER ACCOUNT
            var hasher = new PasswordHasher<User>();

            User managerUser = new User
            {
                Id = Guid.Parse("7BD06FE6-79CA-43A1-862B-446A1466BB93"),
                UserName = "manager@cw.com",
                NormalizedUserName = "MANAGER@CW.COM",
                Email = "manager@cw.com",
                NormalizedEmail = "MANAGER@CW.COM",
                CreatedOn = DateTime.UtcNow,
                LockoutEnabled = true,
                SecurityStamp = "7I5VNHIJTSZNOT3KDWKNFUV5PVYBHGXN"
            };

            managerUser.PasswordHash = hasher.HashPassword(managerUser, "manager");

            builder.Entity<User>().HasData(managerUser);

            builder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid>
                {
                    RoleId = Guid.Parse("297D06E6-C058-486F-A18A-06A971EBFCD7"),
                    UserId = managerUser.Id
                });

            //SEEDING INGREDIENTS
            builder.Entity<Ingredient>().HasData(
                new Ingredient
                {
                    Id = Guid.Parse("91767830-FB0E-4E77-A93A-D01EB2520553"),
                    Name = "Whisky"
                },
                new Ingredient
                {
                    Id = Guid.Parse("AF31C27C-D4E5-4D19-8304-2C649ADB2F49"),
                    Name = "Gin"
                },
                new Ingredient
                {
                    Id = Guid.Parse("8B4157A7-49F0-4487-B800-C569C9EC7DD6"),
                    Name = "Vodka"
                },
                new Ingredient
                {
                    Id = Guid.Parse("F97A5F83-F9DA-43A3-BEF9-67091533CCC9"),
                    Name = "Rum"
                },
                new Ingredient
                {
                    Id = Guid.Parse("4F036905-92AF-4B1B-8879-41B0FA8F1020"),
                    Name = "Tequila"
                },
                new Ingredient
                {
                    Id = Guid.Parse("0303B014-79B5-4044-9994-85AC83F293FC"),
                    Name = "Cointreau"
                },
                new Ingredient
                {
                    Id = Guid.Parse("730BCB1E-ED31-4600-9E42-7019898154B5"),
                    Name = "Cola"
                },
                new Ingredient
                {
                    Id = Guid.Parse("7F5402B0-2136-4ABB-B809-86C1CB502F62"),
                    Name = "Ginger Ale"
                },
                new Ingredient
                {
                    Id = Guid.Parse("F9D9AC89-7C03-4A41-8A1A-B69262F89E16"),
                    Name = "Club Soda"
                },
                new Ingredient
                {
                    Id = Guid.Parse("DD3B4DCD-1E23-4B02-BDF1-859D892A7D89"),
                    Name = "Lemon Sour"
                }
                );
        }
    }
}
