namespace Movies.Application.Commands.ProducerCommands
{
    public class CreateProducerHandler
    {
        public string Producer { get; set; } = null!;
        public int Interval { get; set; }
        public int PreviousWin { get; set; }
        public int FollowingWin { get; set; }
    }
}
