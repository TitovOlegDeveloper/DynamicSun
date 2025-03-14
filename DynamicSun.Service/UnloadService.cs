using DynamicSun.Dal.MsSql.Models;
using DynamicSun.Domain.Abstractions;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSun.Service
{
    public class UnloadService : IUnloadService
    {
        IWeatherQuery _query;


        public UnloadService(IWeatherQuery query)
        {
            _query = query;
        }

        public int GetCountRows(DateTime? firstDate, DateTime? secondDate)
        {
            return _query.CountWeather(firstDate, secondDate);
        }

        public List<Weather> GetResultTable(int pageSize, int pageNum, DateTime? firstDate, DateTime? secondDate)
        {
            return _query.GetWeather(pageSize, pageNum, firstDate, secondDate);
        }
    }
}
