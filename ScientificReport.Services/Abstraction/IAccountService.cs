using System.Collections.Generic;
using System.Threading.Tasks;
using ScientificReport.DAL;
using ScientificReport.DAL.DTO;

namespace ScientificReport.Services.Abstraction
{
    public interface IAccountService
    {
        void Initialize(RegisterDTO model);
        Task<IEnumerable<string>> GetCathedrasNames();
        Task<IEnumerable<string>> GetFacultiesNames();
        int? GetFacultyIdByName(string facultyName);
        IEnumerable<string> GetCathedrasNamesByFacultyId(int facultyId);
        string GetFacultyNameById(int id);
    }
}