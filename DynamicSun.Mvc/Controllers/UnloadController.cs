using DynamicSun.Dal.MsSql.Models;
using DynamicSun.Domain.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DynamicSun.Mvc.Controllers
{
    public class UnloadController : Controller
    {
        ILoadService _loadService;
        IWeatherQuery _dal;

        public UnloadController(ILoadService loadService, IWeatherQuery dal)
        {
            _loadService = loadService;
            _dal = dal;

        }

      
        [HttpGet]
        public ContentResult UnloadReport()
        {
            
            List<object> data = new List<object>()
            {
                new { Id = 1, Name = "Item 1", Value = 10 },
                new { Id = 2, Name = "Item 2", Value = 20 }
            };

 
            string jsonData = JsonConvert.SerializeObject(_dal.GetWeather());

       
            return Content(jsonData, "application/json");

            
        }

    }
}
