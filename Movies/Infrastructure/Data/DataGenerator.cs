namespace Movies.Infrastructure.Data
{
    public class DataGenerator
    {
        private static readonly Random _random = new Random();
        
        private static readonly string[] FirstNames = new[] { "John", "Alice", "Bob" };

        private static readonly string[] LastNames = new[] { "Doe" };

        public static string GenerateRandomName()
        {
            string firstName = FirstNames[_random.Next(FirstNames.Length)];
            string lastName = LastNames[0];
            return $"{firstName} {lastName}";
        }

        public static int GenerateRandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
    }

}
