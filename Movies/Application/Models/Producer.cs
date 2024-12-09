namespace Movies.Application.Models
{
    public class Producer
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Interval { get; set; }
        public int PreviousWin { get; set; }
        public int FollowingWin { get; set; }
    }
}
