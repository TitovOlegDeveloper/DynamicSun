



using DynamicSun.Dal.MsSql.Models;

namespace DynamicSun.Domain.Abstractions
{
    public interface IWeatherQuery
    {
        public Task<string> AddWeather(List<Weather> _weathers);

        public List<Weather> GetWeather(int pageSize, int pageNum, DateTime? firstDate, DateTime? secondDate);
        public int CountWeather(DateTime? firstDate, DateTime? secondDate);
    }

}
