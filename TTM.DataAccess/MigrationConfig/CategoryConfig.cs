using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TTM.Domain;

namespace TTM.DataAccess.MigrationConfig
{
    internal class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(nameof(Category));
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().IsUnicode().HasMaxLength(256);
            builder.Property(c => c.Description).IsRequired(false).IsUnicode().HasMaxLength(256);
            
            builder.HasData(
                new Category
                {
                    Id = 1,
                    Name = "Work",
                    Description = "Professional tasks, projects, deadlines, meetings, and collaborations. Keep track of job-related duties and goals effortlessly."
                },
                new Category
                {
                    Id = 2,
                    Name = "Personal",
                    Description = "Manage errands, appointments, hobbies, self-care, and chores efficiently. Organize daily routines and prioritize personal goals."
                },
                new Category
                {
                    Id = 3,
                    Name = "Health & Fitness",
                    Description = "Stay on top of exercise, meals, medical appointments, therapy, and self-care. Achieve physical and mental well-being goals seamlessly."
                },
                new Category
                {
                    Id = 4,
                    Name = "Education",
                    Description = "Study for exams, complete assignments, attend classes, research topics, and pursue professional development. Support academic and career objectives effectively."
                },
                new Category
                {
                    Id = 5,
                    Name = "Social",
                    Description = "Plan social activities, events, gatherings, and networking. Stay connected with friends and loved ones effortlessly."
                });
        }
    }
}
