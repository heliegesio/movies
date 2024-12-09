using AutoMapper;
using Movies.Application.Commands.ProducerCommands.Request;
using Movies.Application.Queries.ProducerQuerys.Response;
using Movies.Domain.Models;

namespace Movies.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           
            CreateMap<CreateProducerRequest, Producer>();
            CreateMap<ProducerJson, CreateProducerRequest>()
                .ForMember(dst => dst.Name, map => map.MapFrom(src => src.Producer))
                .ForMember(dst => dst.Interval, map => map.MapFrom(src => src.Interval))
                .ForMember(dst => dst.PreviousWin, map => map.MapFrom(src => src.PreviousWin))
                .ForMember(dst => dst.FollowingWin, map => map.MapFrom(src => src.FollowingWin));

            CreateMap<Producer, ListarProducersQueryResponse>();
        }
    }
}
