using AutoMapper;
using MediatR;
using Movies.Infrastructure.Data;

public class ListarProducerQueryHandler : IRequestHandler<ListarProducerQuery, List<ListarProducerQueryResponse>>
{
    private readonly MoviesDbContext _context;
    private readonly IMapper _mapper;

    public ListarProducerQueryHandler(MoviesDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ListarProducerQueryResponse>> Handle(ListarProducerQuery request, CancellationToken cancellationToken)
    {
        // Obtenha a consulta IQueryable
        var producersQuery = _context.Producers.AsQueryable();

        // Use ProjectTo após garantir que você tem um IQueryable
        return await producersQuery
            .ProjectTo<ListarProducerQueryResponse>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}