using SRS.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Services.Interfaces
{
    public interface ICsvService
    {
        byte[] WriteCsv<TData>(CsvModel<TData> model);
    }
}
