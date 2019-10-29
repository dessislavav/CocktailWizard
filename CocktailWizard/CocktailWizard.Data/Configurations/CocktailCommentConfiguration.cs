using CocktailWizard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailWizard.Data.Configurations
{
    public class CocktailCommentConfiguration : IEntityTypeConfiguration<CocktailComment>
    {
        public void Configure(EntityTypeBuilder<CocktailComment> builder)
        {
            builder.HasKey(cc => cc.Id);

            builder.Property(cc => cc.Body)
                .IsRequired();

            builder.HasOne(cc => cc.Cocktail)
                .WithMany(c => c.Comments);

            builder.HasOne(cc => cc.User)
                .WithMany(u => u.CocktailComments);
        }
    }
}
