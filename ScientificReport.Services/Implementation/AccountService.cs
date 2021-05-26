using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<string> GetCathedrasNames()
        {
            return db.Cathedras.GetAllAsync().Result.OrderBy(x => x.Name).ToList().Select(x => x.Name);
        }

        public IEnumerable<string> GetFacultiesNames()
        {
            return db.Faculties.GetAllAsync().Result.OrderBy(x => x.Name).ToList().Select(x => x.Name);
        }
    }
}
