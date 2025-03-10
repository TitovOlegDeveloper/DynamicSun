using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DynamicSun.Mvc.Controllers
{
    public class LoadController : Controller
    {
        [HttpPost]
        public IActionResult UploadFiles()
        {
            return RedirectToAction("Index"); 
        }
      
    }
}
