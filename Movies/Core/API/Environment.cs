using Microsoft.Extensions.Configuration;

namespace Movies.Core.API
{
    public class Environment : IEnvironment
    {
        private readonly IConfiguration _configuration;

        private readonly Dictionary<string, string> _parameters;

        private const string DevelopmentEnvironmentName = "Development";

        public Environment(string name, IConfiguration configuration)
        {
            Name = name.Trim();

            _configuration = configuration;

            _parameters = configuration
                .AsEnumerable()
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public string Name { get; }

        public string this[string key] => _parameters.ContainsKey(key) ? _parameters[key] : string.Empty;

        public T ObtenhaConfiguracoes<T>()
        {
            return _configuration.GetSection(typeof(T).Name).Get<T>();
        }

        public T ObtenhaConfiguracoes<T>(string section)
        {
            return _configuration.GetSection(section).Get<T>();
        }

        public bool IsDevelopment()
        {
            return string.Equals(Name.Trim(), DevelopmentEnvironmentName, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}