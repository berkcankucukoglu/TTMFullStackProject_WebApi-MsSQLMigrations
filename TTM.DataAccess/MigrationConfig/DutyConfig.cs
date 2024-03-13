using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TTM.Domain;

namespace TTM.DataAccess.MigrationConfig
{
    internal class DutyConfig : IEntityTypeConfiguration<Duty>
    {
        public void Configure(EntityTypeBuilder<Duty> builder)
        {
            builder.ToTable(nameof(Duty));
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Name).IsRequired().IsUnicode().HasMaxLength(256);
            builder.Property(d => d.Description).IsRequired(false).IsUnicode().HasMaxLength(256);
            builder.Property(d => d.CreatedDate).IsRequired().HasColumnType("datetime2(0)");
            builder.Property(d => d.LastModifiedDate).IsRequired(false).HasColumnType("datetime2(0)");
            builder.Property(d => d.StartDate).IsRequired(false).HasColumnType("datetime2(0)");
            builder.Property(d => d.EndDate).IsRequired(false).HasColumnType("datetime2(0)");
            builder.Property(d => d.Status).IsRequired();
            builder.Property(d => d.Hours).IsRequired(false).HasColumnType("float");
            builder.HasOne(d => d.User)
                .WithMany(u => u.Duties)
                .HasForeignKey(d => d.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(d => d.Project)
                .WithMany(p => p.Duties)
                .HasForeignKey(d => d.ProjectId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
