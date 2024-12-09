using Movies.Application.Commands.Producer.Request;
using Movies.Application.Commands.Producer.Response;

namespace Movies.Application.Queries
{
    public interface IGetProducersQuery
    {
        CreateProducerResponse Handle(CreateProducerRequest command);
    }
}
