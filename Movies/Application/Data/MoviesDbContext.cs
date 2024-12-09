using Microsoft.EntityFrameworkCore;
using Movies.Application.Models;

namespace Movies.Application.Data
{
    public class MoviesDbContext : DbContext
    {
        public MoviesDbContext(DbContextOptions<MoviesDbContext> options) : base(options)
        {
        }
        public DbSet<Producer> Producers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Producer>().HasKey(m => m.Id);
            base.OnModelCreating(builder);
        }
    }
}
