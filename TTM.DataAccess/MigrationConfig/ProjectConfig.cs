using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TTM.Domain;

namespace TTM.DataAccess.MigrationConfig
{
    internal class ProjectConfig : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable(nameof(Project));
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().IsUnicode().HasMaxLength(256);
            builder.Property(p => p.Description).IsRequired(false).IsUnicode().HasMaxLength(256);
            builder.Property(p => p.CreatedDate).IsRequired().HasColumnType("datetime2(0)");
            builder.Property(p => p.LastModifiedDate).IsRequired(false).HasColumnType("datetime2(0)");
            builder.Property(p => p.StartDate).IsRequired(false).HasColumnType("datetime2(0)");
            builder.Property(p => p.EndDate).IsRequired(false).HasColumnType("datetime2(0)");
            builder.Property(p => p.Status).IsRequired();
            builder.HasOne(p => p.User)
                .WithMany(u => u.Projects)
                .HasForeignKey(p => p.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(p=>p.Category)
                .WithMany(c => c.Projects)
                .HasForeignKey(p => p.CategoryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
