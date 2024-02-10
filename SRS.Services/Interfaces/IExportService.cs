using SRS.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Services.Interfaces
{
    public interface IExportService
    {
        byte[] WriteExcel<TData>(CsvModel<TData> csvModel);

        byte[] WriteCsv<TData>(CsvModel<TData> model);
    }
}
