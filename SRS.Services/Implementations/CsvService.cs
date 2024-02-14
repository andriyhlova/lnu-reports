using CsvHelper;
using CsvHelper.Configuration;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Services.Implementations
{
    public class CsvService : ICsvService
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
    }
}
