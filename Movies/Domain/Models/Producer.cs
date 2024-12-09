namespace Movies.Domain.Models
{
    public class Producer : Model
    {

        public Producer(string name, int interval, int previousWin, int followingWin)
        {
            Id = Guid.NewGuid();
            Name = name;
            Interval = interval;
            PreviousWin = previousWin;
            FollowingWin = followingWin;
        }


        public string Name { get; set; } = null!;
        public int Interval { get; set; }
        public int PreviousWin { get; set; }
        public int FollowingWin { get; set; }


    }
}
