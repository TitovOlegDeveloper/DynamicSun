using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSun.Service
{
    public class ExcelHelper : Helper
    {
        public override bool CheckFileExtension(string fileExtension)
        {
            return fileExtension == ".xlsx"
                 || fileExtension == ".xls"
                 || fileExtension == ".csv"
                 ? true : false;
        }
    }
}
