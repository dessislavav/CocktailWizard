using CocktailWizard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailWizard.Data.Configurations
{
    public class BarConfiguration : IEntityTypeConfiguration<Bar>
    {
        public void Configure(EntityTypeBuilder<Bar> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name)
                .IsRequired();

            builder.Property(b => b.Phone)
                .IsRequired();

            builder.Property(b => b.ImagePath);

            builder.Property(b => b.Address)
                .IsRequired();

            builder.HasMany(b => b.BarCocktails)
                .WithOne(c => c.Bar)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
