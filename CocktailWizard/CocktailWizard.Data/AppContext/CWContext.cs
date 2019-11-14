using CocktailWizard.Data.Configurations;
using CocktailWizard.Data.Entities;
using CocktailWizard.Data.Seeder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace CocktailWizard.Data.AppContext
{
    public class CWContext : IdentityDbContext<User, Role, Guid>
    {
        public CWContext(DbContextOptions<CWContext> options) : base(options)
        {
        }

        public DbSet<Ban> Bans { get; set; }
        public DbSet<Bar> Bars { get; set; }
        public DbSet<BarComment> BarComments { get; set; }
        public DbSet<BarRating> BarRatings { get; set; }
        public DbSet<BarCocktail> BarCocktails { get; set; }
        public DbSet<Cocktail> Cocktails { get; set; }
        public DbSet<CocktailIngredient> CocktailIngredients { get; set; }
        public DbSet<CocktailComment> CocktailComments { get; set; }
        public DbSet<CocktailRating> CocktailRatings { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new BanConfiguration());
            builder.ApplyConfiguration(new BarConfiguration());
            builder.ApplyConfiguration(new BarCommentConfiguration());
            builder.ApplyConfiguration(new BarRatingConfiguration());
            builder.ApplyConfiguration(new BarCocktailConfiguration());
            builder.ApplyConfiguration(new CocktailConfiguration());
            builder.ApplyConfiguration(new CocktailIngredientConfiguration());
            builder.ApplyConfiguration(new CocktailCommentConfiguration());
            builder.ApplyConfiguration(new CocktailRatingConfiguration());
            builder.ApplyConfiguration(new IngredientConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());

            builder.Seeder();

            base.OnModelCreating(builder);
        }
    }
}
