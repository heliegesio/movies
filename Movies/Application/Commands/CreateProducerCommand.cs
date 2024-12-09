namespace Movies.Application.Commands
{
    public class CreateProducerCommand
    {
        public string Producer { get; set; } = null!;
        public int Interval { get; set; }
        public int PreviousWin { get; set; }
        public int FollowingWin { get; set; }
    }
}
