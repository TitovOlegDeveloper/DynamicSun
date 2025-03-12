using DynamicSun.Dal.MsSql.Context;
using DynamicSun.Dal.MsSql.Models;
using DynamicSun.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSun.Dal.Query
{
    public class WeatherQuery : IWeatherQuery
    {
        
        public async Task<string> AddWeather(List<Weather> weathers)
        {
            
            using (var db = new MSContext())
            {

                 db.Weathers.AddRange(weathers);
                 await db.SaveChangesAsync();
                return "Записи успешно добавлены!";
            }
        }

    }
}
