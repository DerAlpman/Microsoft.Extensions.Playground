using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlaygroundServices.Contracts
{
    public interface IWeatherService
    {
        Task<IReadOnlyList<int>> GetFiveDayTemperaturesAsync();
    }
}
