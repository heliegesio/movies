using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Movies.Application.Data;

namespace Movies.Migrations
{
    [DbContext(typeof(MoviesDbContext))]
    public class MoviesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {

            modelBuilder
                .HasDefaultSchema("Business")
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Movies.Application.Models.Producer", b =>
            {
                b.Property<Guid>("Id")
                    .HasColumnType("uuid");

                b.Property<string>("Name")
                    .HasColumnType("text");

                b.Property<DateTime?>("Interval")
                    .HasColumnType("integer");

                b.Property<DateTime?>("PreviousWin")
                    .HasColumnType("integer");

                b.Property<DateTime?>("FollowingWin")
                    .HasColumnType("integer");

            });

            
        }
    }
}
