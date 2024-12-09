using Movies.Application.Commands.Producer.Request;
using Movies.Application.Commands.Producer.Response;
using Movies.Domain.Models;
using Movies.Infrastructure.Repositories;

namespace Movies.Application.Handlers.ProducerHandler
{
    public class CreateProducerHandler: ICreateProducerHandler
    {
        IProducerRepository _repository; 

        public CreateProducerHandler(IProducerRepository repository)
        {
            _repository = repository;
        }

        public CreateProducerResponse Handle(CreateProducerRequest command)
        {
            // Aplicar Fail Fast Validations
            
            // Cria a entidade
            var producer = new Producer(command.Name, command.Interval, command.PreviousWin,command.FollowingWin);

            // Persiste a entidade no banco
            _repository.Adicionar(producer);


            // Retorna a resposta
            return new CreateProducerResponse
            {
                Id = producer.Id,
                Name = producer.Name,
                PreviousWin = producer.PreviousWin,
                FollowingWin = producer.FollowingWin
            };
        }
    }
}
