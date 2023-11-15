using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AestheticShop.Models.Configurations
{
    public class ProductTagConfiguration: IEntityTypeConfiguration<ProductTag>
    {
        public void Configure(EntityTypeBuilder<ProductTag> builder)
        {
           builder.HasOne(pt=>pt.Tag).WithMany(p=>p.ProductTags).HasForeignKey(pt=>pt.TagId);
           builder.HasOne(pt => pt.Product).WithMany(p => p.ProductTags).HasForeignKey(pt => pt.ProductId);
        }
    }
}
