using MediatR;
using Movies.Core.Querys;
using Movies.Domain.Models;

namespace Movies.Application.Queries.ProducerQuerys.Response
{
    public class ListarProducersQueryResponse : IRequest<PagedQueryResult<Producer>>
    {
        public int PageNumber { get; set; } = 0; // Número da página, inicie em 0
        public int PageSize { get; set; } = 10; // Tamanho da página
    }
}
