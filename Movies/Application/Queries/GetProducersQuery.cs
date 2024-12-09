using Movies.Application.Commands.Producer.Request;
using Movies.Application.Commands.Producer.Response;
using Movies.Infrastructure.Repositories;

namespace Movies.Application.Queries
{
    public class GetProducersQuery
    {
        IProducerRepository _repository;

        public GetProducersQuery(IProducerRepository repository)
        {
            _repository = repository;
        }
        public CreateProducerResponse Handle(CreateProducerRequest command)
        {

            return new CreateProducerResponse();// _repository.Obter();
        }
    }
}
