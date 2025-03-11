using DymamicSun.Dal.Ms_sql.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DymamicSun.Dal.Context
{
    public class MSWeatherContext : DbContext
    {
        public DbSet<Weather> Weathers { get; set; }

        public MSWeatherContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseSqlServer("Server=252-584;Database=DynamicSunBase;Integrated Security=True;Encrypt=False;");
          // optionsBuilder.UseSqlServer("Server=DESKTOP-9E9RRAJ\\MSSQLSERVER01;Database=DynamicSunBase;Integrated Security=True;Encrypt=False;");
        }
    }
}
