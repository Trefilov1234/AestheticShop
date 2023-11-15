using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AestheticShop.Models.Configurations
{
    public class TagConfiguration: IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(100);
            builder.HasData(new List<Tag>
            {
                new Tag{Id=1 , Name="Easy going"},
                new Tag{Id=2 , Name="Branded"},
                new Tag{Id=3 , Name="Quality"},
                new Tag{Id=4 , Name="Luxury"},
                new Tag{Id=5 , Name="Choice"},
                new Tag{Id=6 , Name="High class"},
            });
        }
    }
}
