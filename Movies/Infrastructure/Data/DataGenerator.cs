namespace Movies.Infrastructure.Data
{
    public class DataGenerator
    {
        private static readonly Random _random = new Random();
        
        private static readonly string[] FirstNames = new[] { "John", "Alice", "Bob", "Charlie", "David", "Eva", "Frank", "Grace", "Henry", "Isabelle" };

        private static readonly string[] LastNames = new[] { "Doe", "Smith", "Johnson", "Brown", "Wilson", "Adams", "Miller", "Lee", "Martinez", "Harris" };

        public static string GenerateRandomName()
        {
            string firstName = FirstNames[_random.Next(FirstNames.Length)];
            string lastName = LastNames[_random.Next(LastNames.Length)];
            return $"{firstName} {lastName}";
        }

        public static int GenerateRandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
    }

}
