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

namespace Movies.Application.Queries
{
    public class ListarProducerQueryHandler : IRequestHandler<ListarProducerRequest, PagedQueryResult<ListarProducersQueryResponse>>
    {
        IProducerRepository _repository;

        private readonly IMapper _mapper;
        public ListarProducerQueryHandler(IProducerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<PagedQueryResult<ListarProducersQueryResponse>> Handle(ListarProducerRequest request, CancellationToken cancellationToken)
        {
            var filtro = PredicateBuilder.New<Producer>(true);
            if (request.Name != null)
                filtro = filtro.And(x => x.Name.ToLower().Contains(request.Name.ToLower()));

            return await _repository
                .ObterAsNoTracking()
                .Where(filtro)
                .ProjectTo<ListarProducersQueryResponse>(_mapper.ConfigurationProvider)
                .PaginateAsync(request.NumeroDaPagina, request.TamanhoDaPagina, cancellationToken);
            

        }
    }
}