using Movies.Core.Querys;
using Microsoft.EntityFrameworkCore;

namespace Movies.Core.Extensions
{
    public static class PaginationExtension
    {
        public static async Task<PagedQueryResult<TResultItem>> PaginateAsync<TResultItem>(
            this IQueryable<TResultItem> query,
            int numeroDaPagina = 0,
            int tamanhoDaPagina = 10,
            CancellationToken cancellationToken = new())
            where TResultItem : IPagedQueryResultItem
        {
            numeroDaPagina = numeroDaPagina < 0 ? 0 : numeroDaPagina;
            tamanhoDaPagina = tamanhoDaPagina < 1 ? 10 : tamanhoDaPagina;

            var totalElements = await query.CountAsync(cancellationToken);

            var startRow = numeroDaPagina * tamanhoDaPagina;
            var items = await query
                .Skip(startRow)
                .Take(tamanhoDaPagina)
                .ToListAsync(cancellationToken);

            var result = new PagedQueryResult<TResultItem>
            {
                Itens = items,
                Paginacao = new QueryPaginationInfo
                {
                    Numero = numeroDaPagina,
                    TamanhoDaPagina = items.Count,
                    TotalDeElementos = totalElements
                }
            };

            return result;
        }
    }
}
