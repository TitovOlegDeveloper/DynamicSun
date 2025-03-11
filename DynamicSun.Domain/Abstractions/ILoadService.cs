using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSun.Domain.Abstractions
{
    public interface ILoadService
    {
        string LoadFiles(IFormFile[] files);
    }
}
