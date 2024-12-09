using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using Movies.Domain.Models;

namespace Movies.Infrastructure.Data
{
    public class MoviesDbContext : DbContext
    {
        public MoviesDbContext(DbContextOptions<MoviesDbContext> options) : base(options)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MoviesDbContext>();
            optionsBuilder.UseSqlite("Data Source=Movies.db");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
            {
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning);
            });
        }
        public DbSet<Producer> Producers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder
               .Entity<Producer>()
               .ToTable(nameof(Producer))
               .HasKey(t => t.Id);

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
          
            if (await SaveChangesAsync(cancellationToken) <= 0)
            {
                return false;
            }

          

            return true;
        }
    }
}
