using System.Collections.Generic;
using ScientificReport.DAL;
using ScientificReport.DAL.DTO;

namespace ScientificReport.Services.Abstraction
{
    public interface IAccountService
    {
        void Initialize(RegisterDTO model);
        IEnumerable<string> GetCathedrasNames();
        IEnumerable<string> GetFacultiesNames();
    }
}