using DynamicSun.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DynamicSun.Dal.Ms_sql.Models
{
    public class Weather : IWeather
    {
        public int Id { get; set; }
        public DateOnly? Date { get; set; }
        public TimeOnly? Time { get; set; }
        public decimal? Temperature { get; set; } 
        public decimal? AirHumidity { get; set; } 
        public decimal? DewPoint { get; set; } 
        public decimal? Pressure { get; set; } 
        public string? Wind { get; set; } 
        public int? WindSpeed { get; set; } 
        public int? Cloudiness { get; set; } 
        public int? CloudLimit { get; set; } 
        public int? HorizontalVisibility { get; set; } 
        public string? Appearance { get; set; } 
    }
}
