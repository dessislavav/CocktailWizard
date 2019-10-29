using CocktailWizard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailWizard.Data.Configurations
{
    public class BarRatingConfiguration : IEntityTypeConfiguration<BarRating>
    {
        public void Configure(EntityTypeBuilder<BarRating> builder)
        {
            builder.HasKey(br => br.Id);

            builder.Property(br => br.Value)
                .IsRequired();

            builder.HasOne(br => br.User)
                .WithMany(u => u.BarRatings);

            builder.HasOne(br => br.Bar)
                .WithMany(b => b.Ratings);
        }
    }
}
