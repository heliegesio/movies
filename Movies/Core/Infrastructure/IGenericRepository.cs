using Movies.Core.Querys;
using Movies.Domain.Models;
using System.Linq.Expressions;

namespace Movies.Core.Infrastructure
{
    public interface IGenericRepository<TEntity> where TEntity : Model // Garante que TEntity precise herdar de Model
    {
        Task<PagedQueryResult<TEntity>> Listar<TResultItem>(Expression<Func<TEntity, bool>>? filtro = null, int numeroDaPagina = 0, int tamanhoDaPagina = 10, CancellationToken cancellationToken = default(CancellationToken));
        IQueryable<TEntity> Obter();
        Task AdicionarAsync(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
