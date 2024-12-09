using AutoMapper;
using MediatR;
using Movies.Application.Queries.ProducerQuerys.Request;
using Movies.Core.Querys;
using Movies.Domain.Models;
using Movies.Infrastructure.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using LinqKit;
using System.Linq.Expressions;
using Movies.Application.Queries.ProducerQuerys.Response;
using AutoMapper.QueryableExtensions;
using Movies.Core.Extensions;
using System.Data.Entity;

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