using CocktailWizard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailWizard.Data.Configurations
{
    public class CocktailConfiguration : IEntityTypeConfiguration<Cocktail>
    {
        public void Configure(EntityTypeBuilder<Cocktail> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired();

            builder.Property(c => c.ImagePath);

            builder.HasMany(c => c.BarCocktails)
                .WithOne(b => b.Cocktail)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Ratings)
                .WithOne(cr => cr.Cocktail);

            builder.HasMany(c => c.Comments)
                .WithOne(cc => cc.Cocktail);

            builder.HasMany(c => c.CocktailIngredients)
                    .WithOne(ci => ci.Cocktail)
                    .HasForeignKey(ci => ci.CocktailId);
        }
    }
}
