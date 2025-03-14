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
        IUnloadService _unloadService;

        public UnloadController(ILoadService loadService, IUnloadService unloadService)
        {
            _loadService = loadService;
            _unloadService = unloadService;

        }


        private int pageSize = 10;
        [HttpGet("/Unload/UnloadReport/{pageNum}/{firstDate}/{secondDate}")]
        public ContentResult UnloadReport(int pageNum, DateTime? firstDate, DateTime? secondDate)
        {
            return Content(JsonConvert.SerializeObject(_unloadService.GetResultTable(pageSize, pageNum,firstDate,secondDate)), "application/json");
        }



        [HttpGet("/Unload/GetCountWeather/{firstDate}/{secondDate}")]
        public int GetCountWeather(DateTime? firstDate, DateTime? secondDate)
        {
            return _unloadService.GetCountRows(firstDate, secondDate);
        }

    }
}
