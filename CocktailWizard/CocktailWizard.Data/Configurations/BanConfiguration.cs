using CocktailWizard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailWizard.Data.Configurations
{
    public class BanConfiguration : IEntityTypeConfiguration<Ban>
    {
        public void Configure(EntityTypeBuilder<Ban> builder)
        {
            builder.HasKey(b => b.Id); 

            builder.Property(b => b.Description)
                .IsRequired();

            builder.HasOne(b => b.User)
                .WithMany(u => u.Bans);

            builder.Property(b => b.HasExpired)
                .IsRequired();
           
        }
    }
}
