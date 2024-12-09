namespace Movies.Core.Mediator
{
    public interface IMediatorResult
    {
        IEnumerable<string> Errors { get; }

        IMediatorResult AddError(string error);

        IMediatorResult AddErrors(ICollection<string> error);

        bool IsValid();
    }
}
