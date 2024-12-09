using AutoMapper;
using MediatR;
using Movies.Application.Queries.ProducerQuerys.Request;
using Movies.Core.Querys;
using Movies.Domain.Models;
using Movies.Infrastructure.Repositories;

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


            return await _repository.Listar<Producer>(request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}