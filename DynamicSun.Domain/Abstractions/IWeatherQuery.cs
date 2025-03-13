



using DynamicSun.Dal.MsSql.Models;

namespace DynamicSun.Domain.Abstractions
{
    public interface IWeatherQuery
    {
        public Task<string> AddWeather(List<Weather> _weathers);

        public List<Weather> GetWeather();
    }

}
