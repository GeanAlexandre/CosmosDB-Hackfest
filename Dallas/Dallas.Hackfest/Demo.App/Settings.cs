using Microsoft.Extensions.Configuration;

namespace Demo.App
{
    public class Settings : ISettings
    {
        public string ConnectionString { get; }

        private Settings(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("Mongo");
        }

        public static Settings Configure(IConfiguration configuration) => new Settings(configuration);
    }
}
