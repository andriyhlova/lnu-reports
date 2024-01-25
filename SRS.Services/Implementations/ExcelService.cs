using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Services.Implementations
{
    public class ExcelService : IExcelService
    {
        public byte[] ExportToExcelBytes<TData>(CsvModel<TData> csvModel, string sheetName)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(sheetName);

                for (int i = 0; i < csvModel.Header.Count; i++)
                {
                    worksheet.Cell(1, i + 1).Value = csvModel.Header[i];
                }

                int row = 2;
                foreach (var item in csvModel.Data)
                {
                    var properties = item.GetType().GetProperties();
                    for (int i = 0; i < properties.Length; i++)
                    {
                        worksheet.Cell(row, i + 1).Value = properties[i].GetValue(item, null)?.ToString();
                    }

                    row++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
    }
}
