using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScientificReport.DAL.Abstraction;
using ScientificReport.DAL.Models;

namespace ScientificReport.DAL.Repositories.Interfaces
{
    public interface ICathedraReportRepository : IGenericRepository<CathedraReport, int>
    {
    }
}
