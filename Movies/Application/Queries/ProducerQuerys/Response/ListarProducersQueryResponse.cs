using Movies.Core.Querys;

namespace Movies.Application.Queries.ProducerQuerys.Response
{
    public class ListarProducersQueryResponse : IPagedQueryResultItem
    {
        public string Name { get; set; } = null!;
        public int Interval { get; set; }
        public int PreviousWin { get; set; }
        public int FollowingWin { get; set; }

    }
}
