using CocktailWizard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailWizard.Data.Configurations
{
    public class CocktailIngredientConfiguration : IEntityTypeConfiguration<CocktailIngredient>
    {
        public void Configure(EntityTypeBuilder<CocktailIngredient> builder)
        {
            builder.HasKey(ci => new { ci.IngredientId, ci.CocktailId });

            builder.HasOne(cc => cc.Cocktail)
                 .WithMany(c => c.CocktailIngredients)
                 .HasForeignKey(c => c.CocktailId);

            builder.HasOne(cc => cc.Ingredient)
                 .WithMany(c => c.CocktailIngredients)
                 .HasForeignKey(c => c.IngredientId);
        }
    }
}
