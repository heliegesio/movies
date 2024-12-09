using AutoMapper;
using Movies.Application.Commands.ProducerCommands.Request;
using Movies.Domain.Models;

namespace Movies.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           
            CreateMap<CreateProducerRequest, Producer>(); 
        }
    }
}
