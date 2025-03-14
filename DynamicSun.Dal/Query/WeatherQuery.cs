using DynamicSun.Dal.MsSql.Context;
using DynamicSun.Dal.MsSql.Models;
using DynamicSun.Domain.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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

      

        public List<Weather> GetWeather(int pageSize, int pageNum)
        {
            using (var db = new MSContext())
            {
                return db.Weathers.OrderBy(x => x.Id).Skip(pageSize * pageNum).Take(pageSize).ToList();
            }
        }

        public int CountWeather()
        {
            using (var db = new MSContext())
            {
                return db.Weathers.Count();
            }
        }

    }
}
