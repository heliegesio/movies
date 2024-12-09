using Microsoft.EntityFrameworkCore;
using Movies.Core.Querys;
using Movies.Domain.Models;
using System.Linq.Expressions;

namespace Movies.Core.Infrastructure
{
    public interface IGenericRepository<TEntity> where TEntity : Model // Garante que TEntity precise herdar de Model
    {
        
        IQueryable<TEntity> Obter();
        IQueryable<TEntity> ObterAsNoTracking();
        Task AdicionarAsync(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
