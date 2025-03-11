using DynamicSun.Domain.Abstractions;
using DynamicSun.Domain.Entities;
using Microsoft.AspNetCore.Http;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSun.Service
{
    public class LoadService : ILoadService
    {


        Helper _helper = new ExcelHelper();

        public string LoadFiles(IFormFile[] files)
        {
            try
            {
                if (!_helper.CheckNotNull(files)) return "Пожалуйста, выберите хотя бы один файл Excel.";
                foreach (var file in files)
                {
                    if (file != null && file.Length > 0)
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        string fileExtension = Path.GetExtension(fileName).ToLower();
                        if (_helper.CheckFileExtension(fileExtension))
                        {
                            try
                            {
                                using (var stream = new MemoryStream())
                                {

                                    file.CopyTo(stream);
                                    stream.Position = 0;


                                    IWorkbook workbook = null;

                                    if (fileExtension == ".xlsx")
                                    {
                                        workbook = new XSSFWorkbook(stream);
                                    }
                                    else if (fileExtension == ".xls")
                                    {
                                        workbook = new HSSFWorkbook(stream);
                                    }
     

                                    if (workbook != null)
                                    {
                                        for (int i = 0; i < workbook.NumberOfSheets;i++)
                                        {
                                            ISheet sheet = workbook.GetSheetAt(i); 
                                            if (sheet != null)
                                            {
                                                int rowCount = sheet.LastRowNum; 
                                                IRow firstRow = sheet.GetRow(0);
                                                int colCount = firstRow != null ? firstRow.LastCellNum : 0;

                                                for (int row = 4; row <= rowCount; row++)
                                                {
                                                    IRow currentRow = sheet.GetRow(row);

                                                    if (currentRow != null)
                                                    {
                                                       
                                                            

                                                            object cellValue = null; 

                                                            if (cell != null) 
                                                            {
                                                                Console.WriteLine($"Row: {row + 1}, Value: {cellValue} + {cell2}");  // Row и Col +1 для удобства отображения (индексация с 1)
                                                            }

                                                        
                                                    }
                                                }
                                            }
                                        }

                                        
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                return $"Ошибка при обработке файла {fileName}: {ex.Message}";
                            }
                        }
                        else
                        {
                            return $"Неверный формат файла: {fileName}.  Разрешены только .xlsx, .xls";
                        }
                    }
                }
                return "Файлы успешно обработаны!";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public void AddModel(
            string date,
            string time,
            string temeperature,
            string air,
            string dew, 
            string pressure,
            string wind, 
            string speed,
            string cloudiness,
            string limit,
            string visibility,
            string appearance
            )
        {
            Weather weather = new Weather()
            {
                Date = DateOnly.FromDateTime(Convert.ToDateTime(date)) != null ? DateOnly.FromDateTime(Convert.ToDateTime(date)) : null,
                Time = TimeOnly.FromDateTime(Convert.ToDateTime(time)) != null ? TimeOnly.FromDateTime(Convert.ToDateTime(time)) : null,
                Temperature = Convert.ToDecimal(temeperature) != null ? Convert.ToDecimal(temeperature) : null,
                AirHumidity = Convert.ToDecimal(air) != null ? Convert.ToDecimal(air) : null,
                DewPoint = Convert.ToDecimal(dew) != null ? Convert.ToDecimal(dew) : null,
                Pressure = Convert.ToDecimal(pressure) != null ? Convert.ToDecimal(pressure) : null,
                Wind = Convert.ToString(wind) != null ? Convert.ToString(wind) : "",
                WindSpeed = Convert.ToInt32(speed) != null ? Convert.ToInt32(speed) : 0,
                Cloudiness = Convert.ToInt32(cloudiness) != null ? Convert.ToInt32(cloudiness) : 0,
                CloudLimit = Convert.ToInt32(limit) != null ? Convert.ToInt32(limit) : 0,
                HorizontalVisibility = Convert.ToInt32(visibility) != null ? Convert.ToInt32(visibility) : 0,
                Appearance = Convert.ToString(appearance) != null ? Convert.ToString(appearance) : "",
            };
        }
    }
}
