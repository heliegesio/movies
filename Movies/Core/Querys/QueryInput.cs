using Movies.Core.Mediator;

namespace Movies.Core.Querys
{
    public class QueryInput<TQueryResult> : MediatorInput<TQueryResult>
        where TQueryResult : QueryResult
    {
    }
}
