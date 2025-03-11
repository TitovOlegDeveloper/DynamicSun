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
                                                        string appearanceValue = string.Empty;
                                                        ICell appearanceCell = currentRow.GetCell(11);

                                                        if (appearanceCell != null)
                                                        {
                                                            appearanceValue = appearanceCell.ToString(); // Теперь безопасно вызывать ToString()
                                                        }
                                                        else
                                                        {
                                                            // Обработка случая, когда ячейка отсутствует (NULL)
                                                            Console.WriteLine("Ячейка 11 отсутствует в строке.");
                                                            appearanceValue = string.Empty; // Или другое значение по умолчанию
                                                        }


                                                        Weather weather = new Weather
                                                        {
                                                            // ... другие свойства ...
                                                            Appearance = appearanceValue // Используем значение, полученное с проверкой на null
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
    }
}
