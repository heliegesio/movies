using Movies.Application.Commands.Producer.Request;
using Movies.Application.Commands.Producer.Response;

namespace Movies.Application.Handlers
{
    public interface ICreateProducerHandler
    {
        CreateProducerResponse Handle(CreateProducerRequest command);
    }
}
