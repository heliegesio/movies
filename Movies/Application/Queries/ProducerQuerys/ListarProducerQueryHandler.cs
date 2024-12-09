using AutoMapper;
using MediatR;
using Movies.Application.Queries.ProducerQuerys.Request;
using Movies.Core.Querys;
using Movies.Domain.Models;
using Movies.Infrastructure.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using LinqKit;
using System.Linq.Expressions;

namespace Movies.Application.Queries
{
    public class ListarProducerQueryHandler : IRequestHandler<ListarProducerRequest, PagedQueryResult<Producer>>
    {
        IProducerRepository _repository;

        private readonly IMapper _mapper;
        public ListarProducerQueryHandler(IProducerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<PagedQueryResult<Producer>> Handle(ListarProducerRequest request, CancellationToken cancellationToken)
        {
            var filtro = PredicateBuilder.New<Producer>(true);
            if (request.Name != null)
                filtro = filtro.And(x => x.Name.ToLower().Contains(request.Name.ToLower()));


            return await _repository.Listar<Producer>(filtro, request.PageNumber, request.PageSize, cancellationToken);

        }
    }
}