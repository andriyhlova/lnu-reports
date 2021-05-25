using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScientificReport.DAL.Abstraction;
using ScientificReport.DAL.Implementation;
using ScientificReport.DAL.Models;
using ScientificReport.DAL.Repositories.Interfaces;

namespace ScientificReport.DAL.Repositories.Realizations
{
    public class ReportRepository : BaseRepository<Report, int>, IReportRepository
    {
        public ReportRepository(IDbContext context) : base(context)
        {
        }
    }
}
