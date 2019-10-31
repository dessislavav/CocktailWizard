using CocktailWizard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailWizard.Data.Configurations
{
    public class BarRatingConfiguration : IEntityTypeConfiguration<BarRating>
    {
        public void Configure(EntityTypeBuilder<BarRating> builder)
        {
            builder.HasKey(ci => new { ci.BarId, ci.UserId });

            builder.Property(br => br.Value)
                .IsRequired();

            builder.HasOne(br => br.User)
                .WithMany(u => u.BarRatings)
                .HasForeignKey(u => u.UserId);

            builder.HasOne(br => br.Bar)
                .WithMany(b => b.Ratings)
                .HasForeignKey(b => b.BarId);
        }
    }
}
