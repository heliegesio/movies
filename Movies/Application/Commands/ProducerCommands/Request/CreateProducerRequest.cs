using MediatR;
using Movies.Application.Commands.ProducerCommands.Response;
using Movies.Domain.Models;

namespace Movies.Application.Commands.ProducerCommands.Request
{
    public class CreateProducerRequest : IRequest<CreateProducerResponse>
    {
        public string Name { get; set; } = null!;
        public int Interval { get; set; }
        public int PreviousWin { get; set; }
        public int FollowingWin { get; set; }
    }
}
