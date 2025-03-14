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
                return  "Записи успешно добавлены!";
            }
        }

      

        public List<Weather> GetWeather(int pageSize, int pageNum, DateTime? firstDate, DateTime? secondDate)
        {
            using (var db = new MSContext())
            {
                return  firstDate == null && secondDate == null ?
                    db.Weathers.OrderBy(x => x.Id).Skip(pageSize * pageNum).Take(pageSize).ToList()
               : db.Weathers
                .OrderBy(x => x.Id)
                .Where(x => x.Date >= firstDate && x.Date <= secondDate)
                .Skip(pageSize * pageNum)
                .Take(pageSize)
                .ToList();
            }
        }

        public int CountWeather(DateTime? firstDate, DateTime? secondDate)
        {
            using (var db = new MSContext())
            {
                return firstDate == null && secondDate == null ?
                 db.Weathers.Count()
               : db.Weathers
                .Where(x => x.Date >= firstDate && x.Date <= secondDate)
                .Count();
                
            }
        }

    }
}
