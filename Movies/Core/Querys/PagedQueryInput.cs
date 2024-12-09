using Microsoft.AspNetCore.Mvc;

namespace Movies.Core.Querys
{
    public class PagedQueryInput<TQueryResult>
         : QueryInput<TQueryResult> where TQueryResult : QueryResult
    {
        [FromQuery]
        public int TamanhoDaPagina { get; set; }

        [FromQuery]
        public int NumeroDaPagina { get; set; }
    }
}
