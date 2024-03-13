using Microsoft.EntityFrameworkCore;
using TTM.DataAccess.MigrationConfig;
using TTM.Domain;

namespace TTM.DataAccess
{
    public class TTMContext : DbContext
    {
        /*
        I can bring connection string here like this:
            private const string _connectionString = "Server=.; Database=TTM; Integrated Security=true;";

        then override the onConfiguring:
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                base.OnConfiguring(optionsBuilder);
                optionsBuilder.UseSqlServer(_connectionString);
            }
        however,
        I put connection string in appsettings.json and used dependency injection for showcasing how it is done.
        */
        public TTMContext(DbContextOptions options) : base(options)
        {
            //This is here because of dependency injection. Connection string comes here as option.
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Duty> Duties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategoryConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new ProjectConfig());
            modelBuilder.ApplyConfiguration(new DutyConfig());
        }
    }
}
