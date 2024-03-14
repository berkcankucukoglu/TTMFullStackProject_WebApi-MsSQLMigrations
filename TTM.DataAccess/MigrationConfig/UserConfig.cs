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
            builder.Property(u=> u.Gender).IsRequired(false).HasColumnType("smallint");

            builder.HasData(
                new User
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "johndoe@mail.com",
                    Password = "12345",
                    Gender = Gender.None,
                },
                new User
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "janedoe@mail.com",
                    Password = "54321",
                    Gender = Gender.Female,
                });
        }
    }
}
