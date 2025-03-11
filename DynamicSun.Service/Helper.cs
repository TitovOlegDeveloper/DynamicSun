using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSun.Service
{
    public abstract class Helper
    {
        public bool CheckNotNull(IFormFile[] file)
        {
            return (file != null && file.Length > 0) ? true : false;
            
        }
        public abstract bool CheckFileExtension(string fileExtension);


    }
}
