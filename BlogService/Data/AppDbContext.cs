using BlogService.Entity;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace BlogService.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<PostEntity> Posts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostEntity>()
                .Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<PostEntity>()
                .Property(e => e.LastUpdated)
                .HasComputedColumnSql("GETDATE()");
        }
    }
}
