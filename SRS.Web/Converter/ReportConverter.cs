using SRS.Domain.Entities;
using SRS.Web.Models.Shared;
using System.Linq;
using UserManagement.Models.Reports;

namespace UserManagement.Converter
{
    public class ReportConverter
    {
        public static CathedraReportViewModel ConvertToViewModel(CathedraReport report)
        {
            var viewModel = new CathedraReportViewModel()
            {
                Id = report.Id,
                Protocol = report.Protocol,
                Date = report.Date,
                AchivementSchool = report.AchivementSchool,
                AllDescriptionBudgetTheme = report.AllDescriptionBudgetTheme,
                AllDescriptionHospDohovirTheme = report.AllDescriptionHospDohovirTheme,
                AllDescriptionThemeInWorkTime = report.AllDescriptionThemeInWorkTime,
                ApplicationAndPatentsOnInventionBudgetTheme = report.ApplicationAndPatentsOnInventionBudgetTheme,
                ApplicationAndPatentsOnInventionHospDohovirTheme = report.ApplicationAndPatentsOnInventionHospDohovirTheme,
                ApplicationAndPatentsOnInventionThemeInWorkTime = report.ApplicationAndPatentsOnInventionThemeInWorkTime,
                ApplicationOnInvention = report.ApplicationOnInvention,
                BudgetThemeId = report.BudgetTheme?.Id,
                ConferencesInUniversity = report.ConferencesInUniversity,
                CooperationWithAcadamyOfScience = report.CooperationWithAcadamyOfScience,
                CooperationWithForeignScientificInstitution = report.CooperationWithForeignScientificInstitution,
                CVBudgetTheme = report.CVBudgetTheme,
                CVHospDohovirTheme = report.CVHospDohovirTheme,
                CVThemeInWorkTime = report.CVThemeInWorkTime,
                DefenseOfCoworkers = report.DefenseOfCoworkers,
                DefenseOfDoctorantsAndAspirants = report.DefenseOfDoctorantsAndAspirants,
                DefensesOfCoworkersBudgetTheme = report.DefensesOfCoworkersBudgetTheme,
                DefensesOfCoworkersHospDohovirTheme = report.DefensesOfCoworkersHospDohovirTheme,
                DefensesOfCoworkersThemeInWorkTime = report.DefensesOfCoworkersThemeInWorkTime,
                DefenseWithSpecialPeople = report.DefenseWithSpecialPeople,
                HospDohovirThemeId = report.HospDohovirTheme?.Id,
                Materials = report.Materials,
                OtherBudgetTheme = report.OtherBudgetTheme,
                OtherFormsOfScientificWork = report.OtherFormsOfScientificWork,
                OtherHospDohovirTheme = report.OtherHospDohovirTheme,
                OtherThemeInWorkTime = report.OtherThemeInWorkTime,
                Patents = report.Patents,
                PropositionForNewForms = report.PropositionForNewForms,
                StudentsWorks = report.StudentsWorks,
                ThemeInWorkTimeId = report.ThemeInWorkTime?.Id
            };

            viewModel.PrintedPublicationBudgetTheme = report.PrintedPublicationBudgetTheme.Select(x => new CheckboxListItem() { Id = x.Id, Checked = false, Name = x.Name }).ToList();
            viewModel.PrintedPublicationHospDohovirTheme = report.PrintedPublicationHospDohovirTheme.Select(x => new CheckboxListItem() { Id = x.Id, Checked = false, Name = x.Name }).ToList();
            viewModel.PrintedPublicationThemeInWorkTime = report.PrintedPublicationThemeInWorkTime.Select(x => new CheckboxListItem() { Id = x.Id, Checked = false, Name = x.Name }).ToList();

            return viewModel;
        }


        public static CathedraReport ConvertToEntity(CathedraReportViewModel reportViewModel)
        {
            var report = new CathedraReport()
            {
                Protocol = reportViewModel.Protocol,
                Date = reportViewModel.Date,
                AchivementSchool = reportViewModel.AchivementSchool,
                AllDescriptionBudgetTheme = reportViewModel.AllDescriptionBudgetTheme,
                AllDescriptionHospDohovirTheme = reportViewModel.AllDescriptionHospDohovirTheme,
                AllDescriptionThemeInWorkTime = reportViewModel.AllDescriptionThemeInWorkTime,
                ApplicationAndPatentsOnInventionBudgetTheme = reportViewModel.ApplicationAndPatentsOnInventionBudgetTheme,
                ApplicationAndPatentsOnInventionHospDohovirTheme = reportViewModel.ApplicationAndPatentsOnInventionHospDohovirTheme,
                ApplicationAndPatentsOnInventionThemeInWorkTime = reportViewModel.ApplicationAndPatentsOnInventionThemeInWorkTime,
                ApplicationOnInvention = reportViewModel.ApplicationOnInvention,
                ConferencesInUniversity = reportViewModel.ConferencesInUniversity,
                CooperationWithAcadamyOfScience = reportViewModel.CooperationWithAcadamyOfScience,
                CooperationWithForeignScientificInstitution = reportViewModel.CooperationWithForeignScientificInstitution,
                CVBudgetTheme = reportViewModel.CVBudgetTheme,
                CVHospDohovirTheme = reportViewModel.CVHospDohovirTheme,
                CVThemeInWorkTime = reportViewModel.CVThemeInWorkTime,
                DefenseOfCoworkers = reportViewModel.DefenseOfCoworkers,
                DefenseOfDoctorantsAndAspirants = reportViewModel.DefenseOfDoctorantsAndAspirants,
                DefensesOfCoworkersBudgetTheme = reportViewModel.DefensesOfCoworkersBudgetTheme,
                DefensesOfCoworkersHospDohovirTheme = reportViewModel.DefensesOfCoworkersHospDohovirTheme,
                DefensesOfCoworkersThemeInWorkTime = reportViewModel.DefensesOfCoworkersThemeInWorkTime,
                DefenseWithSpecialPeople = reportViewModel.DefenseWithSpecialPeople,
                Materials = reportViewModel.Materials,
                OtherBudgetTheme = reportViewModel.OtherBudgetTheme,
                OtherFormsOfScientificWork = reportViewModel.OtherFormsOfScientificWork,
                OtherHospDohovirTheme = reportViewModel.OtherHospDohovirTheme,
                OtherThemeInWorkTime = reportViewModel.OtherThemeInWorkTime,
                Patents = reportViewModel.Patents,
                PropositionForNewForms = reportViewModel.PropositionForNewForms,
                StudentsWorks = reportViewModel.StudentsWorks,
            };

            return report;
        }
    }
}