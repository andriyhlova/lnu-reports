using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScientificReport.DAL.Abstraction;
using ScientificReport.DAL.DTO;
using ScientificReport.DAL.Enums;
using ScientificReport.DAL.Models;
using ScientificReport.Services.Abstraction;

namespace ScientificReport.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private IUnitOfWork db;
        private IEmailService emailService;
        public AccountService(IEmailService emailService, IUnitOfWork db)
        {
            this.emailService = emailService;
            this.db = db;
        }

        public void Initialize(RegisterDTO model)
        {
            var u = db.Users.GetAllAsync().Result.First(x => x.UserName == model.Email);
            u.Cathedra = db.Cathedras.GetAllAsync().Result.FirstOrDefault(x => x.Name.Equals(model.Cathedra));
            foreach (var i in Enum.GetNames(typeof(Language)))
            {
                u.I18nUserInitials.Add(new I18nUserInitials()
                {
                    Language = (Language)Enum.Parse(typeof(Language), i),
                    FirstName = "",
                    LastName = "",
                    FathersName = "",
                    User = u,
                });
            }
            db.SaveChanges();
        }

        public async Task<IEnumerable<string>> GetCathedrasNames()
        {
            var cathedras = await db.Cathedras.GetAllAsync();
            return cathedras.OrderBy(x => x.Name).ToList().Select(x => x.Name);
        }

        public IEnumerable<string> GetCathedrasNamesByFacultyId(int facultyId)
        {
            return db.Cathedras.GetAllAsync().Result.Where(x => x.Faculty.Id == facultyId).OrderBy(x => x.Name).Select(x => x.Name).ToList();
        }

        public async Task<IEnumerable<string>> GetFacultiesNames()
        {
            var faculties = await db.Faculties.GetAllAsync();
            return faculties.OrderBy(x => x.Name).ToList().Select(x => x.Name);
        }

        public int? GetFacultyIdByName(string facultyName)
        {
            return db.Faculties.GetAllAsync().Result.FirstOrDefault(x => x.Name.Equals(facultyName))?.Id;
        }

        public string GetFacultyNameById(int id)
        {
            return db.Faculties.GetAllAsync().Result.First(x => x.Id == id).Name;
        }
    }
}
