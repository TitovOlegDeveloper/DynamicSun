using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSun.Domain.Entities
{
    public class Weather
    {
        public int Id { get; set; }     
        public DateOnly? Date { get; set; }  
        public TimeOnly? Time { get; set; } 
        public decimal? Temperature { get;set; } // Температура
        public decimal? AirHumidity { get;set; } // Отн. влажность воздуха, %
        public decimal? DewPoint { get;set; } // Точка росы, гр
        public decimal? Pressure { get;set; } // Атм. давление, мм рт.ст.
        public string? Wind { get;set; } // Направление ветра
        public int? WindSpeed { get;set; } // Направление ветра, м/с
        public int? Cloudiness { get;set; } // Обачность, %
        public int? CloudLimit { get;set; } // Нижняя граница облачности
        public int? HorizontalVisibility { get;set; } // Горизонтальная видимость
        public string? Appearance { get;set; } // Погодное явление








    }
}
