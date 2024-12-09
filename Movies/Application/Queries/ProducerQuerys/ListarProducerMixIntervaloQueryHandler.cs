using AutoMapper;
using MediatR;
using Movies.Application.Queries.ProducerQuerys.Request;
using Movies.Application.Queries.ProducerQuerys.Response;
using Movies.Domain.Models;
using Movies.Infrastructure.Repositories;
using System.Data.Entity;

namespace Movies.Application.Queries
{
    public class ListarProducerMinIntervaloQueryHandler : IRequestHandler<ProducerMinIntervalRequest, ProducerIntervalResponse>
    {
        IProducerRepository _repository;

        public ListarProducerMinIntervaloQueryHandler(IProducerRepository repository)
        {
            _repository = repository;
        }
        public async Task<ProducerIntervalResponse> Handle(ProducerMinIntervalRequest request, CancellationToken cancellationToken)
        {
            var producerMinInterval = _repository
                .Obter()
                .ToList()
                .GroupBy(p => p.Name)
                .Select(group => new ProducerIntervalResponse()
                {
                    Name = group.Key,
                    Interval = group.Min(p => p.Interval)
                }).OrderBy(x => x.Interval)
            .First();


            return producerMinInterval;


        }
    }
}