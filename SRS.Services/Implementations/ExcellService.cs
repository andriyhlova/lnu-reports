using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;

namespace SRS.Services.Implementations
{
    public class ExcellService : IExcelService
    {
        public byte[] ExportToExcelBytes<TData>(CsvModel<TData> csvModel)
        {
            if (csvModel == null || csvModel.Data == null)
            {
                throw new ArgumentNullException(nameof(csvModel));
            }

            byte[] excelBytes;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(memoryStream, SpreadsheetDocumentType.Workbook))
                {
                    WorkbookPart workbookPart = spreadsheetDocument.AddWorkbookPart();
                    workbookPart.Workbook = new Workbook();

                    WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();

                    // Create SheetData and then assign it to the Worksheet
                    SheetData sheetData = new SheetData();
#pragma warning disable S3220 // Method calls should not resolve ambiguously to overloads with "params"
                    worksheetPart.Worksheet = new Worksheet(sheetData);
#pragma warning restore S3220 // Method calls should not resolve ambiguously to overloads with "params"

                    Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild(new Sheets());

                    Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Sheet1" };
#pragma warning disable S3220 // Method calls should not resolve ambiguously to overloads with "params"
                    sheets.Append(sheet);
#pragma warning restore S3220 // Method calls should not resolve ambiguously to overloads with "params"

                    WriteDataToSheet(csvModel.Data, sheetData);

                    spreadsheetDocument.Close();
                }

                excelBytes = memoryStream.ToArray();
            }

            return excelBytes;
        }

        private static void WriteDataToSheet<TData>(IList<TData> data, SheetData sheetData)
        {
            PropertyInfo[] properties = typeof(TData).GetProperties();

            Row headerRow = new Row();
            foreach (PropertyInfo property in properties)
            {
                var displayNameAttribute = (DisplayNameAttribute)Attribute.GetCustomAttribute(property, typeof(DisplayNameAttribute));
                string columnName = displayNameAttribute?.DisplayName ?? property.Name;

                Cell cell = new Cell();
                cell.DataType = CellValues.String;
                cell.CellValue = new CellValue(columnName);

                headerRow.AppendChild(cell);
            }

            sheetData.AppendChild(headerRow);

            foreach (TData item in data)
            {
                Row row = new Row();

                foreach (var property in typeof(TData).GetProperties())
                {
                    Cell cell = new Cell();
                    cell.DataType = CellValues.String;

                    cell.CellValue = new CellValue(property.GetValue(item)?.ToString() ?? string.Empty);

                    row.AppendChild(cell);
                }

                sheetData.AppendChild(row);
            }
        }
    }
}
