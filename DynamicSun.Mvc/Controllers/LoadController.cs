using DynamicSun.Domain.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace DynamicSun.Mvc.Controllers
{
    public class LoadController : Controller
    {
        ILoadService _loadService;

        public LoadController(ILoadService loadService)
        {
            _loadService = loadService;
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
