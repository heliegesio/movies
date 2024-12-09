using Movies.Domain.Models;

namespace Movies.Core.Infrastructure
{
    public interface IGenericRepository<TEntity> where TEntity : Model // Garante que TEntity precise herdar de Model
    {
        IQueryable<TEntity> Obter();
        IQueryable<TEntity> ObterAsNoTracking();
        void Adicionar(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
