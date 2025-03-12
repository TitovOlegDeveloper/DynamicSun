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

        MSContext _db;

        public LoadService(MSContext db)
        {
            _db = db;
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

                                                        ICell dateCell = currentRow.GetCell(0);
                                                        ICell timeCell = currentRow.GetCell(1);
                                                        ICell tempCell = currentRow.GetCell(2);
                                                        ICell airCell = currentRow.GetCell(3);
                                                        ICell dewCell = currentRow.GetCell(4);
                                                        ICell pressureCell = currentRow.GetCell(5);
                                                        ICell windCell = currentRow.GetCell(6);
                                                        ICell speedCell = currentRow.GetCell(7);
                                                        ICell cloudCell = currentRow.GetCell(8);
                                                        ICell limitCell = currentRow.GetCell(9);
                                                        ICell visibilityCell = currentRow.GetCell(10);
                                                        ICell appearanceCell = currentRow.GetCell(11);

                                                        string? appearanceValue = appearanceCell != null ? appearanceCell.ToString() : string.Empty;
                                                        int? visibilityValue = TryParseInt(visibilityCell);
                                                        int? limitValue = TryParseInt(limitCell);
                                                        int? cloudValue = TryParseInt(cloudCell);
                                                        int? speedValue = TryParseInt(speedCell);
                                                        string? windValue = windCell != null ? windCell.ToString() : string.Empty;
                                                        int? pressureValue = TryParseInt(pressureCell);
                                                        decimal? dewValue = TryParseDecimal(dewCell);
                                                        decimal? airValue = TryParseDecimal(airCell);
                                                        decimal? tempValue = TryParseDecimal(tempCell);
                                                        string? timeValue = windCell != null ? timeCell.ToString() : string.Empty;
                                                        DateTime? dateValue = TryParseDate(dateCell);




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

                using (var db = new MSContext())
                {
                    db.Weathers.AddRange(_weathers);
                    db.SaveChanges();

                }

                return "Файлы успешно обработаны!";
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
