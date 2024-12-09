using MediatR;
using Movies.Domain.Models;

namespace Movies.Application.Queries.ProducerQuerys.Request
{
    public class ProducerTestIntervalRequest : IRequest<Producer>
    {
        public string Type { get; set; } = null!;

    }
}
