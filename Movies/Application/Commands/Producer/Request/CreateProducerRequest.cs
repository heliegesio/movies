namespace Movies.Application.Commands.Producer.Request
{
    public class CreateProducerRequest
    {
        public string Name { get; set; } = null!;
        public int Interval { get; set; }
        public int PreviousWin { get; set; }
        public int FollowingWin { get; set; }
    }
}
