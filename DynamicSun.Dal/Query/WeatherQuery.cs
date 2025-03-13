using DynamicSun.Dal.MsSql.Context;
using DynamicSun.Dal.MsSql.Models;
using DynamicSun.Domain.Abstractions;
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

      

        public List<Weather> GetWeather()
        {
            using (var db = new MSContext())
            {
               
               // return db.Weathers.OrderBy(x => x.Date).Skip(pageSize * pageNum).Take(pageSize).ToList();
                return db.Weathers.OrderBy(x => x.Date).Skip(10 * 1).Take(10).ToList();

             
        

                // return db.Weathers.OrderBy(x => x.Date).ToList();


            }
        }

    }
}
