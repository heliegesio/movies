using Microsoft.EntityFrameworkCore;
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

        public IQueryable<TEntity> ObterAsNoTracking() => Obter().AsNoTrackingWithIdentityResolution();

        public async Task AdicionarAsync(TEntity entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
        }


        public void Update(TEntity entity) => _context.Entry(entity).State = EntityState.Modified;

        public void Remove(TEntity entity) => _context.Remove(entity);

        

        //public async Task<PagedQueryResult<TEntity>> Listar<TResultItem>(Expression<Func<TEntity, bool>> filtro = null, int numeroDaPagina = 0, int tamanhoDaPagina = 5, CancellationToken cancellationToken = default(CancellationToken))
        //{
        //    var query = this.Obter();

        //    if (filtro != null)
        //    {
        //        query = query.Where(filtro);
        //    }

        //    numeroDaPagina = ((numeroDaPagina >= 0) ? numeroDaPagina : 0);
        //    tamanhoDaPagina = ((tamanhoDaPagina < 1) ? 5 : tamanhoDaPagina);

        //    int totalElements = await query.CountAsync(cancellationToken);
        //    int count = numeroDaPagina * tamanhoDaPagina;
        //    List<TEntity> list = await query.Skip(count).Take(tamanhoDaPagina).ToListAsync(cancellationToken);

        //    return new PagedQueryResult<TEntity>
        //    {
        //        Itens = list,
        //        Paginacao = new QueryPaginationInfo
        //        {
        //            Numero = numeroDaPagina,
        //            TamanhoDaPagina = list.Count,
        //            TotalDeElementos = totalElements
        //        }
        //    };
        //}

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}