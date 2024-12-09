using Microsoft.EntityFrameworkCore;
using Movies.Core.Querys;
using Movies.Domain.Models;
using Movies.Infrastructure.Data;
namespace Movies.Core.Infrastructure
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity>, IDisposable where TEntity : Model
    {
        private readonly MoviesDbContext _context;

        protected GenericRepository(MoviesDbContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> Obter() => _context.Set<TEntity>().AsQueryable();


        public void Adicionar(TEntity entity)
        {
            _context.Add(entity);
            _context.SaveChangesAsync();
        }


        public void Update(TEntity entity) => _context.Entry(entity).State = EntityState.Modified;

        public void Remove(TEntity entity) => _context.Remove(entity);

        public async Task<PagedQueryResult<TEntity>> Listar<TResultItem>(int numeroDaPagina = 0, int tamanhoDaPagina = 10, CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = this.Obter();
            numeroDaPagina = ((numeroDaPagina >= 0) ? numeroDaPagina : 0);
            tamanhoDaPagina = ((tamanhoDaPagina < 1) ? 10 : tamanhoDaPagina);
            int totalElements = await query.CountAsync(cancellationToken);
            int count = numeroDaPagina * tamanhoDaPagina;
            List<TEntity> list = await query.Skip(count).Take(tamanhoDaPagina).ToListAsync(cancellationToken);
            return new PagedQueryResult<TEntity>
            {
                Itens = list
            };
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}