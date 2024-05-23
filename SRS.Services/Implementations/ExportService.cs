using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using SRS.Services.Extensions;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using SRS.Services.Models.CsvModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace SRS.Services.Implementations
{
    public class ExportService : IExportService
    {
        public byte[] WriteCsv<TData>(CsvModel<TData> model)
        {
            using (var m = new MemoryStream())
            {
                using (var writer = new StreamWriter(m, Encoding.UTF8))
                {
                    using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        Delimiter = ";"
                    }))
                    {
                        csv.WriteRecords(model.Data);
                    }
                }

                return m.ToArray();
            }
        }

        public byte[] WriteExcel<TData>(CsvModel<TData> csvModel)
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
                }

                excelBytes = memoryStream.ToArray();
            }

            return excelBytes;
        }

        private static void WriteDataToSheet<TData>(IList<TData> data, SheetData sheetData)
        {
            PropertyInfo[] properties = typeof(TData).GetProperties().OrderBy(p => p.GetCustomAttribute<IndexAttribute>().Index).ToArray();

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

                foreach (var property in properties)
                {
                    Cell cell = new Cell();
                    cell.DataType = CellValues.String;

                    cell.CellValue = new CellValue(XmlConvert.EncodeName(property.GetValue(item)?.ToString()) ?? string.Empty);

                    row.AppendChild(cell);
                }

                sheetData.AppendChild(row);
            }
        }
    }
}
