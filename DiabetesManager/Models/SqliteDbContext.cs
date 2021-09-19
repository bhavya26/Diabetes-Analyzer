


using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DiabetesManager.Models
{
    public class SqliteDbContext : DbContext
    {
        public DbSet<DbManager> DbManager { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=db.sqlite", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map table names
            modelBuilder.Entity<DbManager>().ToTable("DbManager");
            modelBuilder.Entity<DbManager>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.Avg);
                entity.Property(e => e.Comments);
                entity.Property(e => e.Date);
                entity.Property(e => e.Date1);
                entity.Property(e => e.Glucose);
                entity.Property(e => e.Max);
                entity.Property(e => e.Min);
                entity.Property(e => e.Reading);
                entity.Property(e => e.Reading1);
                entity.Property(e => e.Time);
                entity.Property(e => e.Value);
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}