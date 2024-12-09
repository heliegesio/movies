using MediatR;

namespace Movies.Core.Mediator
{
    public interface IMediatorInput<out TMediatorResult> : IRequest<TMediatorResult> where TMediatorResult : IMediatorResult
    {

    }
}
