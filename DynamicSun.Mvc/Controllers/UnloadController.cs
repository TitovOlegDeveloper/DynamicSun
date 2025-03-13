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
            return Content(JsonConvert.SerializeObject(_dal.GetWeather()), "application/json");
        }

    }
}
