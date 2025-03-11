using DynamicSun.Domain.Abstractions;
using Microsoft.AspNetCore.Http;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
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
                                                        //Weather weather =  Weather.AddWeatherRow
                                                        //     (
                                                        //     currentRow.GetCell(0).ToString() ?? "",
                                                        //    currentRow.GetCell(1).ToString() ?? "",
                                                        //    currentRow.GetCell(2).ToString() ?? "",
                                                        //    currentRow.GetCell(3).ToString() ?? "",
                                                        //    currentRow.GetCell(4).ToString() ?? "",
                                                        //    currentRow.GetCell(5).ToString() ?? "",
                                                        //    currentRow.GetCell(6).ToString() ?? "",
                                                        //    currentRow.GetCell(7).ToString() ?? "",
                                                        //    currentRow.GetCell(8).ToString() ?? "",
                                                        //    currentRow.GetCell(9).ToString() ?? "",
                                                        //    currentRow.GetCell(10).ToString() ?? "",
                                                        //    currentRow.GetCell(11).ToString() ?? ""
                                                        //    );
                                                        
                                                       
                                                        //if(weather != null)
                                                        //_weathers.Add(weather);
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

               // _db.Weathers.AddRange(_weathers);
               // _db.SaveChanges();

                return "Файлы успешно обработаны!";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
