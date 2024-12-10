using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Movies.Application.Queries.ProducerQuerys.Request;
using Movies.Application.Queries.ProducerQuerys.Response;
using Movies.Core.Extensions;
using Movies.Infrastructure.Repositories;

namespace Movies.Application.Queries
{
    public class ListarProducerMinIntervaloQueryHandler : IRequestHandler<ProducerMinIntervalRequest, ProducerIntervalResponse>
    {
        IProducerRepository _repository;
        private readonly IMapper _mapper;

        public ListarProducerMinIntervaloQueryHandler(IProducerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProducerIntervalResponse> Handle(ProducerMinIntervalRequest request, CancellationToken cancellationToken)
        {
            var producerMinInterval = await _repository
              .ObterAsNoTracking()
              .ProjectTo<ListarProducersQueryResponse>(_mapper.ConfigurationProvider)
              .ListarAsync(cancellationToken);

            var producer = producerMinInterval
                .Select(p => new ProducerIntervalResponse() 
                {
                    Name = p.Name,
                    Interval = p.FollowingWin - p.PreviousWin // Calcula a diferença de anos
                })
            .OrderBy(x => x.Interval) // Ordena pela menor diferença
            .First(); 


            return producer;


        }
    }
}