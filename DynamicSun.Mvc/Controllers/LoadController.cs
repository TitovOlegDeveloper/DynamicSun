using DynamicSun.Dal.MsSql.Models;
using DynamicSun.Domain.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace DynamicSun.Mvc.Controllers
{
    public class LoadController : Controller
    {
        ILoadService _loadService;
        IWeatherQuery _dal;
        public LoadController(ILoadService loadService, IWeatherQuery dal)
        {
            _loadService = loadService;
            _dal = dal;
        }

        [HttpPost]
        public IActionResult AddFiles(IFormFile[] files)

        {
            string result = _loadService.LoadFiles(files);
            ViewBag.Message = result;
            return View("../Home/LoadReport");
           
        }
    }
}
