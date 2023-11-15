using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AestheticShop.Models.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(100);
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Description)
                .HasMaxLength(1000);
        }
    }
}
