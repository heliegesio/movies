namespace Movies.Domain.Models
{
    public class Producer : Model
    {
        public string Name { get; set; } = null!;
        public int Interval { get; set; }
        public int PreviousWin { get; set; }
        public int FollowingWin { get; set; }
    }
}
