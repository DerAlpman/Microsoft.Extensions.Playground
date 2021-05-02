using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PlaygroundServices.Contracts;

namespace ConsoleHostedService
{
    public sealed class ConsoleHostedService : IHostedService
    {
        #region FIELDS
        private readonly ILogger _Logger;
        private readonly IHostApplicationLifetime _AppLifeTime;
        private readonly IWeatherService _WeatherService;
        #endregion

        #region CONSTRUCTION
        public ConsoleHostedService(
            ILogger<ConsoleHostedService> logger, IHostApplicationLifetime applicationLifetime,
            IWeatherService weatherService)
        {
            _Logger = logger;
            _AppLifeTime = applicationLifetime;
            _WeatherService = weatherService;
        }
        #endregion

        #region IHostedService
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _Logger.LogDebug($"Starting with arguments: {string.Join(" ", Environment.GetCommandLineArgs())}");

            _AppLifeTime.ApplicationStarted.Register(() =>
            {
                Task.Run(async () =>
                {
                    try
                    {
                        _Logger.LogInformation("START ASYNC");

                        IReadOnlyList<int> temperatures = await _WeatherService.GetFiveDayTemperaturesAsync();
                        for (int i = 0; i < temperatures.Count; i++)
                        {
                            _Logger.LogInformation($"{DateTime.Today.AddDays(i).DayOfWeek}: {temperatures[i]}");
                        }
                    }
                    catch (Exception ex)
                    {
                        _Logger.LogError(ex, "Unhandled Exception");
                    }
                    finally
                    {
                        _AppLifeTime.StopApplication();
                    }
                });
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _Logger.LogInformation("STOP ASYNC");

            return Task.CompletedTask;
        }
        #endregion 
    }
}
