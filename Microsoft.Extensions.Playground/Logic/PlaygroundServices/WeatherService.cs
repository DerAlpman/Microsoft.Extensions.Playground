using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationOptions;
using Microsoft.Extensions.Options;
using PlaygroundServices.Contracts;

namespace PlaygroundServices
{
    public sealed class WeatherService : IWeatherService
    {
        #region FIELDS
        private readonly IOptions<WeatherOptions> _WeatherOptions;
        #endregion

        #region CONSTRUCTION
        public WeatherService(IOptions<WeatherOptions> weatherOptions)
        {
            _WeatherOptions = weatherOptions;
        }
        #endregion

        #region IWeatherService
        public Task<IReadOnlyList<int>> GetFiveDayTemperaturesAsync()
        {
            int[] temperatures = new[] { 76, 78, 90, 103, 215 };

            if (_WeatherOptions.Value.Unit.Equals("C", StringComparison.OrdinalIgnoreCase))
            {
                for (int i = 0; i < temperatures.Length; i++)
                {
                    temperatures[i] = (int)Math.Round((temperatures[i] - 32) / 1.8);
                }
            }

            return Task.FromResult<IReadOnlyList<int>>(temperatures);
        }
        #endregion
    }
}
