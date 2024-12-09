using MediatR;
using Movies.Application.Queries.ProducerQuerys.Response;
using Movies.Core.Querys;
using Movies.Domain.Models;

namespace Movies.Application.Queries.ProducerQuerys.Request
{
    public class ListarProducerRequest : PagedQueryInput<PagedQueryResult<ListarProducersQueryResponse>>
    {
        public string? Name { get; set; }

    }
}
