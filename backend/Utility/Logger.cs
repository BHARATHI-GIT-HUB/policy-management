using Microsoft.Extensions.Configuration;
using Serilog.Sinks.File;
using Serilog;

namespace RepositryAssignement.Utility
{
    public class Logger
    {
        public void BuildConfigure()
        {
            var configuration = new ConfigurationBuilder()
                           .AddJsonFile("appsettings.json").Build();

            Log.Logger = new LoggerConfiguration().
                    ReadFrom.Configuration(configuration)
                    .CreateLogger();

        }
    }
}

