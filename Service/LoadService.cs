using DynamicSun.Domain.Abstractions;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class LoadService : ILoadService
    {

        public string LoadFiles(IFormFile[] files)
        {
            try
            {
                if (files == null || files.Length == 0)
                {
                    return "Пожалуйста, выберите хотя бы один файл Excel.";
                    
                }

                foreach (var file in files)
                {
                    if (file != null && file.Length > 0)
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        string fileExtension = Path.GetExtension(fileName).ToLower();
                        if (fileExtension == ".xlsx" || fileExtension == ".xls" || fileExtension == ".csv")
                        {
                            try
                            {
                                using (var stream = new MemoryStream())
                                {
                                    file.CopyTo(stream);
                                    stream.Position = 0; // Очень важно!

                                    using (ExcelPackage package = new ExcelPackage(stream))
                                    {
                                        ExcelWorksheet worksheet = package.Workbook.Worksheets[1]; //Первый лист (индексация с 1)
                                        int rowCount = worksheet.Dimension?.Rows ?? 0; //rowCount
                                        int colCount = worksheet.Dimension?.Columns ?? 0; //colCount
                                                                                          //Проходим по всем строкам и столбцам, начиная со второй строчки
                                        for (int row = 2; row <= rowCount; row++)
                                        {
                                            for (int col = 1; col <= colCount; col++)
                                            {
                                                object cellValue = worksheet.Cells[row, col].Value;
                                                //Console.WriteLine($"Row: {row}, Col: {col}, Value: {cellValue}");
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
                            return $"Неверный формат файла: {fileName}.  Разрешены только .xlsx, .xls и .csv";
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
    }
}
