
using Movies.Application.Models;

namespace Movies.Core.Infrastructure
{
    public interface IGenericRepository<TEntity> where TEntity : Model
    {
        IUnitOfWork UnitOfWork { get; }
        void Adicionar(TEntity entity);
        IQueryable<TEntity> Obter();
        IQueryable<TEntity> ObterAsNoTracking();
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
