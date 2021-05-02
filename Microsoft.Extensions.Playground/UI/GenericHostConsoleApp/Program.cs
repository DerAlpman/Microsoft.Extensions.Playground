using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using ApplicationOptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlaygroundServices;
using PlaygroundServices.Contracts;

namespace GenericHostConsoleApp
{
    public sealed class Program
    {
        private static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                .UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .ConfigureLogging(logging =>
                {
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services
                    .AddHostedService<ConsoleHostedService.ConsoleHostedService>()
                    .AddSingleton<IWeatherService, WeatherService>();

                    services
                    .AddOptions<WeatherOptions>().Bind(hostContext.Configuration.GetSection("Weather"));
                })
                .RunConsoleAsync();
        }
    }
}
