using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AestheticShop.Models.Configurations
{
    public class CategoryConfiguration: IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(100);
            builder.HasData(new List<Category>
            {
                new Category{Id=1 , Name="Electronic"},
                new Category{Id=2 , Name="Clothes"},
                new Category{Id=3 , Name="Cosmetics"},
                new Category{Id=4 , Name="Toys"},
                new Category{Id=5 , Name="Drugs"},
                new Category{Id=6 , Name="Furniture"},
            });
        }
    }
}
