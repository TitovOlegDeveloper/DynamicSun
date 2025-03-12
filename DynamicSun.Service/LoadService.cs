using DynamicSun.Dal.MsSql.Context;
using DynamicSun.Dal.MsSql.Models;
using DynamicSun.Domain.Abstractions;
using Microsoft.AspNetCore.Http;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICell = NPOI.SS.UserModel.ICell;

namespace DynamicSun.Service
{
    public class LoadService : ILoadService
    {

        IWeatherQuery _query;


        public LoadService(IWeatherQuery query)
        {
            _query = query;
        }

        Helper _helper = new ExcelHelper();
        List<Weather> _weathers = new List<Weather>();



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
                                        for (int i = 0; i < workbook.NumberOfSheets; i++)
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
                                                        string? appearanceValue = currentRow.GetCell(11) != null ? currentRow.GetCell(11).ToString() : string.Empty;
                                                        int? visibilityValue = TryParseInt(currentRow.GetCell(10));
                                                        int? limitValue = TryParseInt(currentRow.GetCell(9));
                                                        int? cloudValue = TryParseInt(currentRow.GetCell(8));
                                                        int? speedValue = TryParseInt(currentRow.GetCell(7));
                                                        string? windValue = currentRow.GetCell(6) != null ? currentRow.GetCell(6).ToString() : string.Empty;
                                                        int? pressureValue = TryParseInt(currentRow.GetCell(5));
                                                        decimal? dewValue = TryParseDecimal(currentRow.GetCell(4));
                                                        decimal? airValue = TryParseDecimal(currentRow.GetCell(3));
                                                        decimal? tempValue = TryParseDecimal(currentRow.GetCell(2));
                                                        string? timeValue = currentRow.GetCell(1) != null ? currentRow.GetCell(1).ToString() : string.Empty;
                                                        DateTime? dateValue = TryParseDate(currentRow.GetCell(0));

                                                        Weather weather = new Weather
                                                        {
                                                            HorizontalVisibility = visibilityValue,
                                                            Appearance = appearanceValue,
                                                            CloudLimit = limitValue,
                                                            Cloudiness = cloudValue,
                                                            WindSpeed = speedValue,
                                                            Wind = windValue,
                                                            Pressure = pressureValue,
                                                            DewPoint = dewValue,
                                                            AirHumidity = airValue,
                                                            Temperature = tempValue,
                                                            Time = timeValue,
                                                            Date = dateValue

                                                        };

                                                        if (weather != null)
                                                            _weathers.Add(weather);
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

                return _query.AddWeather(_weathers).Result.ToString();
            

               
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        
        static int? TryParseInt(ICell cell)
        {
            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
            {
                if (int.TryParse(cell.ToString(), out int parsed))
                {
                    return parsed;
                }
                else
                {
                    Console.WriteLine($"Неверный формат видимости: {cell.ToString()}");
                    return null;
                }
            }
            else
            {
                Console.WriteLine("Ячейка видимости пуста или отсутствует.");
                return null;
            }
        }

        static decimal? TryParseDecimal(ICell cell)
        {
            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
            {
                if (decimal.TryParse(cell.ToString(), out decimal parsed))
                {
                    return parsed;
                }
                else
                {
                    Console.WriteLine($"Неверный формат видимости: {cell.ToString()}");
                    return null;
                }
            }
            else
            {
                Console.WriteLine("Ячейка видимости пуста или отсутствует.");
                return null;
            }
        }

        static DateTime? TryParseDate(ICell cell)
        {
            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
            {
                if (DateTime.TryParse(cell.ToString(), out DateTime parsed))
                {
                    return parsed;
                }
                else
                {
                    Console.WriteLine($"Неверный формат видимости: {cell.ToString()}");
                    return null;
                }
            }
            else
            {
                Console.WriteLine("Ячейка видимости пуста или отсутствует.");
                return null;
            }
        }
    }
}
