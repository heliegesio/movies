﻿namespace Movies.Domain.Models
{
    public class ProducerJson
    {
        public string Producer { get; set; } = null!;
        public int Interval { get; set; }
        public int PreviousWin { get; set; }
        public int FollowingWin { get; set; }
    }
}