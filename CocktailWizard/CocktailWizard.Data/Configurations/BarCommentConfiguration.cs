using CocktailWizard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailWizard.Data.Configurations
{
    public class BarCommentConfiguration : IEntityTypeConfiguration<BarComment>
    {
        public void Configure(EntityTypeBuilder<BarComment> builder)
        {
            builder.HasKey(bc => bc.Id);

            builder.Property(bc => bc.Body)
                .IsRequired();

            builder.HasOne(bc => bc.User)
                .WithMany(u => u.BarComments);

            builder.HasOne(bc => bc.Bar)
                .WithMany(b => b.Comments);
        }
    }
}
