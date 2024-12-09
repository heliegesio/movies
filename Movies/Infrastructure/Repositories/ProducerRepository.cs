using Movies.Core.Infrastructure;
using Movies.Domain.Models;
using Movies.Infrastructure.Data;

namespace Movies.Infrastructure.Repositories
{

    public class ProducerRepository : GenericRepository<Producer>, IProducerRepository
    {
        public ProducerRepository(MoviesDbContext context) : base(context)
        {
        }
    }
 
}
