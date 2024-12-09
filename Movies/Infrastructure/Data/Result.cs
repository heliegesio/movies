namespace Movies.Infrastructure.Data
{
    public class Result
    {
        public Prize ProducerWithLongestInterval { get; set; }=null!;
        public Prize ProducerWithShortestInterval { get; set; } = null!;
    }
}
