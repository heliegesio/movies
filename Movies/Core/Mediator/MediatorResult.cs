namespace Movies.Core.Mediator
{
    public class MediatorResult : IMediatorResult
    {
        protected readonly List<string> _errors = new();

        public virtual IMediatorResult AddError(string error)
        {
            _errors.Add(error);

            return this;
        }

        public virtual IMediatorResult AddErrors(ICollection<string> error)
        {
            _errors.AddRange(error);

            return this;
        }

        public bool IsValid() => !Errors.Any();

        public IEnumerable<string> Errors => _errors;
    }
}
