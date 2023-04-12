using BlogService.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogService.Data
{
    public class AppDbContext : DbContext
    {
        DbSet<PostEntity>? posts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { 
        }
    }
}
