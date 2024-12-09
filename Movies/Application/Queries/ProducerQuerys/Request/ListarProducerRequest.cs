using MediatR;
using Movies.Core.Querys;
using Movies.Domain.Models;

namespace Movies.Application.Queries.ProducerQuerys.Request
{
    public class ListarProducerRequest : IRequest<PagedQueryResult<Producer>>
    {
        public string? Name { get; set; }

        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 5;
    }
}
