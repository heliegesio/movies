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


            modelBuilder.Entity<Producer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Interval).IsRequired();
                entity.Property(e => e.PreviousWin).IsRequired();
                entity.Property(e => e.FollowingWin).IsRequired();
            });

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
