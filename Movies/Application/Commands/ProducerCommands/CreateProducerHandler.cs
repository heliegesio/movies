using AutoMapper;
using MediatR;
using Movies.Application.Commands.ProducerCommands.Request;
using Movies.Application.Commands.ProducerCommands.Response;
using Movies.Domain.Models;
using Movies.Infrastructure.Repositories;
using System.Formats.Asn1;

namespace Movies.Application.Commands.ProducerCommands
{
    public class CreateProducerHandler : IRequestHandler<CreateProducerRequest, CreateProducerResponse>
    {
        private readonly IProducerRepository _repository;
        private readonly IMapper _mapper;

        public CreateProducerHandler(IProducerRepository repository,
            IMapper mapper
            )
        {
            _mapper=mapper;
            _repository = repository;
        }

        public async Task<CreateProducerResponse> Handle(CreateProducerRequest request, CancellationToken cancellationToken)
        {

            var producer = _mapper.Map<Producer>(request);
            await _repository.AdicionarAsync(producer);           

            return  new CreateProducerResponse() { Id= producer.Id };
        }
    }
}
