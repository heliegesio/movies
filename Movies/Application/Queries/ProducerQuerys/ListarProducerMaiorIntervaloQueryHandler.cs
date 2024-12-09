using AutoMapper;
using MediatR;
using Movies.Application.Queries.ProducerQuerys.Request;
using Movies.Domain.Models;
using Movies.Infrastructure.Repositories;

namespace Movies.Application.Queries
{
    public class ListarProducerMaiorIntervaloQueryHandler : IRequestHandler<ProducerTestIntervalRequest, Producer>
    {
        IProducerRepository _repository;

        private readonly IMapper _mapper;
        public ListarProducerMaiorIntervaloQueryHandler(IProducerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Producer> Handle(ProducerTestIntervalRequest request, CancellationToken cancellationToken)
        {          

            var producerWithShortestInterval = _repository
                .Obter()
                .OrderBy(p => p.Interval)
                .FirstOrDefault();

            return producerWithShortestInterval;
            

        }
    }
}