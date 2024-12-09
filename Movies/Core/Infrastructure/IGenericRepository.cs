using Movies.Core.Querys;
using Movies.Domain.Models;

namespace Movies.Core.Infrastructure
{
    public interface IGenericRepository<TEntity> where TEntity : Model // Garante que TEntity precise herdar de Model
    {
        Task<PagedQueryResult<TEntity>> Listar<TResultItem>(int numeroDaPagina = 0, int tamanhoDaPagina = 10, CancellationToken cancellationToken = default(CancellationToken));
        IQueryable<TEntity> Obter();
        void Adicionar(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
