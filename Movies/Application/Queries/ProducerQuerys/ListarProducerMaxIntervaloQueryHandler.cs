using AutoMapper;
using MediatR;
using Movies.Application.Queries.ProducerQuerys.Request;
using Movies.Application.Queries.ProducerQuerys.Response;
using Movies.Domain.Models;
using Movies.Infrastructure.Repositories;
using System.Data.Entity;

namespace Movies.Application.Queries
{
    public class ListarProducerMaxIntervaloQueryHandler : IRequestHandler<ProducerMaxIntervalRequest, ProducerIntervalResponse>
    {
        IProducerRepository _repository;

        public ListarProducerMaxIntervaloQueryHandler(IProducerRepository repository)
        {
            _repository = repository;
        }
        public async Task<ProducerIntervalResponse> Handle(ProducerMaxIntervalRequest request, CancellationToken cancellationToken)
        {
            var producerMaxInterval = _repository
                .Obter()
                .ToList()
                .GroupBy(p => p.Name)
                .Select(group => new ProducerIntervalResponse()
                {
                    Name = group.Key,
                    Interval = group.Max(p => p.Interval)
                }).OrderByDescending(x => x.Interval)
            .First();


            return producerMaxInterval;


        }
    }
}