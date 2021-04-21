using System;
using System.Threading.Tasks;
using DataClasses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PlaygroundConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
            await host.StartAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, configuration) =>
                {
                    configuration.Sources.Clear();

                    IHostEnvironment env = hostingContext.HostingEnvironment;

                    configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                    IConfigurationRoot configurationRoot = configuration.Build();

                    TransientFaultHandlingOptions transientFaultHandlingOptions = new();
                    configurationRoot.GetSection(nameof(TransientFaultHandlingOptions)).Bind(transientFaultHandlingOptions);

                    MailOptions mailOptions = new();
                    configurationRoot.GetSection(nameof(MailOptions)).Bind(mailOptions);

                    Console.WriteLine($"TransientFaultHandlingOptions.Enabled={transientFaultHandlingOptions.Enabled}");
                    Console.WriteLine($"TransientFaultHandlingOptions.AutoRetryDelay={transientFaultHandlingOptions.AutoRetryDelay}");
                    Console.WriteLine($"MailOptions.Sender={mailOptions.Sender}");
                    foreach (var recipient in mailOptions.Recipients)
                    {
                        Console.WriteLine($"MailOptions.Recipients={recipient}");
                    }
                });
        }
    }
}
