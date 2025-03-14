﻿using DynamicSun.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSun.Dal.MsSql.Models
{
    public class Weather : IWeather
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string? Time { get; set; }
        public decimal? Temperature { get; set; }
        public decimal? AirHumidity { get; set; }
        public decimal? DewPoint { get; set; }
        public int? Pressure { get; set; }
        public string? Wind { get; set; }
        public int? WindSpeed { get; set; }
        public int? Cloudiness { get; set; }
        public int? CloudLimit { get; set; }
        public int? HorizontalVisibility { get; set; }
        public string? Appearance { get; set; }
    }
}
