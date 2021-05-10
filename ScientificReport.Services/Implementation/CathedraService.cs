using ScientificReport.DAL.Models;
using ScientificReport.DAL.Repositories.Interfaces;
using ScientificReport.DAL.Repositories.Realizations;
using ScientificReport.Services.Abstraction;

namespace ScientificReport.Services.Implementation
{
    public class CathedraService : ServiceBase<Cathedra, int>, ICathedraService
    {
        public CathedraService(ICathedraRepository repository): base(repository)
        {
        }
    }
}