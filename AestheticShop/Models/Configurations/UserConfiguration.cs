using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AestheticShop.Models.Configurations
{
    public class UserConfiguration: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Login)
                .HasMaxLength(100);
            builder.Property(x => x.PasswordHash)
                .HasMaxLength(100);
        }
    }
}
