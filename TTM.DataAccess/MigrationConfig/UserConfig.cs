using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TTM.Domain;

namespace TTM.DataAccess.MigrationConfig
{
    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.FirstName).IsRequired().IsUnicode().HasMaxLength(256);
            builder.Property(u => u.LastName).IsRequired(false).IsUnicode().HasMaxLength(256);
            
            builder.Property(u => u.Email).IsRequired().IsUnicode().HasMaxLength(256);
            builder.HasIndex(u => u.Email).IsUnique();

            builder.Property(u => u.Password).IsRequired().HasMaxLength(8193); ;
            builder.Property(u => u.Gender).IsRequired(false).HasColumnType("smallint");
            builder.Property(u => u.Role).IsRequired().HasColumnType("smallint");
            builder.Property(u => u.Token).IsRequired(false).HasMaxLength(8193);
        }
    }
}
