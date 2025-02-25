﻿using CocktailWizard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailWizard.Data.Configurations
{
    public class CocktailRatingConfiguration : IEntityTypeConfiguration<CocktailRating>
    {
        public void Configure(EntityTypeBuilder<CocktailRating> builder)
        {
            builder.HasKey(ci => new { ci.CocktailId, ci.UserId });

            builder.Property(cr => cr.Value)
                .IsRequired();

            builder.HasOne(cr => cr.User)
                .WithMany(u => u.CocktailRatings)
                .HasForeignKey(u => u.UserId);

            builder.HasOne(cr => cr.Cocktail)
                .WithMany(c => c.Ratings)
                .HasForeignKey(c => c.CocktailId);
        }
    }
}
