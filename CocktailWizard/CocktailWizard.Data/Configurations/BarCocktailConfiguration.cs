using CocktailWizard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailWizard.Data.Configurations
{
    public class BarCocktailConfiguration : IEntityTypeConfiguration<BarCocktail>
    {
        public void Configure(EntityTypeBuilder<BarCocktail> builder)
        {
            builder.HasKey(ci => new { ci.CocktailId, ci.BarId });

            builder.HasOne(bc => bc.Bar)
                .WithMany(u => u.BarCocktails)
                .HasForeignKey(u => u.BarId);

            builder.HasOne(bc => bc.Cocktail)
                .WithMany(b => b.BarCocktails)
                .HasForeignKey(b => b.CocktailId);
        }
    }
}
