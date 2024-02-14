using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Services.Models
{
    public class CsvModel<TData>
    {
        public List<string> Header { get; set; }

        public IList<TData> Data { get; set; }
    }
}
