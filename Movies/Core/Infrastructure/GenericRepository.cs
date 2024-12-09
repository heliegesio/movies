using Microsoft.EntityFrameworkCore;
using Movies.Infrastructure.Data;

using Movies.Application.Models;
namespace Movies.Core.Infrastructure
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity>, IDisposable where TEntity : Model
    {
        private readonly MoviesDbContext _context;

        protected GenericRepository(MoviesDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => (IUnitOfWork)_context;

        public IQueryable<TEntity> Obter() => _context.Set<TEntity>().AsQueryable();

        public IQueryable<TEntity> ObterAsNoTracking() => Obter().AsNoTrackingWithIdentityResolution();

        public void Adicionar(TEntity entity) => _context.Add(entity);

        public void Update(TEntity entity) => _context.Entry(entity).State = EntityState.Modified;

        public void Remove(TEntity entity) => _context.Remove(entity);

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
