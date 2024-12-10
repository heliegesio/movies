using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Movies.Application.Queries.ProducerQuerys.Request;
using Movies.Application.Queries.ProducerQuerys.Response;
using Movies.Core.Extensions;
using Movies.Domain.Models;
using Movies.Infrastructure.Repositories;
using System.Data.Entity;

namespace Movies.Application.Queries
{
    public class ListarProducerMaxIntervaloQueryHandler : IRequestHandler<ProducerMaxIntervalRequest, ProducerIntervalResponse>
    {
        IProducerRepository _repository;
        private readonly IMapper _mapper;

        public ListarProducerMaxIntervaloQueryHandler(
            IProducerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ProducerIntervalResponse> Handle(ProducerMaxIntervalRequest request, CancellationToken cancellationToken)
        {

            var producerMaxInterval = await _repository
               .ObterAsNoTracking()
               .ProjectTo<ListarProducersQueryResponse>(_mapper.ConfigurationProvider)
               .ListarAsync(cancellationToken);

            var producer = producerMaxInterval
                 .Select(p => new ProducerIntervalResponse()
                 {
                     Name = p.Name,
                     Interval = (p.FollowingWin - p.PreviousWin) - p.Interval
                 })
                .OrderByDescending(x => x.Interval)
                .First(); 
               


            return producer;


        }
    }
}