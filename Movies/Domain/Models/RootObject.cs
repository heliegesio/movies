namespace Movies.Domain.Models
{
    public class RootObject
    {
        public List<ProducerJson> Min { get; set; } = null!;
        public List<ProducerJson> Max { get; set; } = null!;
    }
}
