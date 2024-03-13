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
            builder.Property(u => u.Password).IsRequired().IsUnicode();

            //Default users for initializing db. There will be no CRUD on this entity.
            builder.HasData(
                new User
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "johndoe@mail.com",
                    Password = "12345"
                });
        }
    }
}
