namespace Movies.Core.Querys
{
    public class PagedQueryResult<TEntity>
    {
        public IEnumerable<TEntity> Itens { get; set; } = null!;

        public QueryPaginationInfo Paginacao { get; set; } = null!;
    }
}
