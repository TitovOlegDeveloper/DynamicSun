using DynamicSun.Dal.MsSql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSun.Domain.Abstractions
{
    public interface IUnloadService
    {
        public int GetCountRows(DateTime? firstDate, DateTime? secondDate);
        public List<Weather> GetResultTable(int pageSize, int pageNum, DateTime? firstDate, DateTime? secondDate);
    }
}
