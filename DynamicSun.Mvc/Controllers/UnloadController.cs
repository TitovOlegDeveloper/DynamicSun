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
        [HttpGet("/Unload/UnloadReport/{pageNum}/{firstDate}/{secondDate}")]
        public ContentResult UnloadReport(int pageNum, DateTime? firstDate, DateTime? secondDate)
        {
            return Content(JsonConvert.SerializeObject(_dal.GetWeather(pageSize, pageNum, firstDate, secondDate)), "application/json");
        }



        [HttpGet("/Unload/GetCountWeather/{firstDate}/{secondDate}")]
        public int GetCountWeather(DateTime? firstDate, DateTime? secondDate)
        {
            return _dal.CountWeather(firstDate, secondDate);
        }

    }
}
