namespace Movies.Core.API
{
    public interface IEnvironment
    {
        string Name { get; }

        string this[string key] { get; }

        bool IsDevelopment();

        T ObtenhaConfiguracoes<T>();

        T ObtenhaConfiguracoes<T>(string section);
    }
}