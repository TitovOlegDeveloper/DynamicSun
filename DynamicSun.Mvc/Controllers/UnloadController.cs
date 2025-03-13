using DynamicSun.Dal.MsSql.Models;
using DynamicSun.Domain.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using NPOI.SS.UserModel;

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


        private int pageSize = 10;
        [HttpGet]
        public ContentResult UnloadReport(int pageNum)
        {
            @ViewData["NumPage"] = pageNum;
            Console.WriteLine(@ViewData["NumPage"]);
            Console.WriteLine("f");
            return Content(JsonConvert.SerializeObject(_dal.GetWeather(pageSize, pageNum)), "application/json");
        }

    }
}
