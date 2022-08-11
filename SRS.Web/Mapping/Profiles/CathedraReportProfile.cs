using System.Linq;
using AutoMapper;
using SRS.Services.Models.CathedraReportModels;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.ReportModels;
using SRS.Web.Models.CathedraReports;
using SRS.Web.Models.Shared;

namespace SRS.Services.Mapping.Profiles
{
    public class CathedraReportProfile : Profile
    {
        public CathedraReportProfile()
        {
            CreateMap<CathedraReportFilterViewModel, CathedraReportFilterModel>()
                .IncludeBase<DepartmentFilterViewModel, DepartmentFilterModel>();

            CreateMap<CathedraReportBudgetThemeViewModel, CathedraReportBudgetThemeModel>()
                .ForMember(dest => dest.PrintedPublicationBudgetThemeIds, opts => opts.MapFrom(src => src.PrintedPublicationBudgetTheme.Where(x => x.Checked).Select(x => x.Id)));

            CreateMap<CathedraReportInTimeThemeViewModel, CathedraReportInTimeThemeModel>()
                .ForMember(dest => dest.PrintedPublicationThemeInWorkTimeIds, opts => opts.MapFrom(src => src.PrintedPublicationThemeInWorkTime.Where(x => x.Checked).Select(x => x.Id)));

            CreateMap<CathedraReportHospDohovirThemeViewModel, CathedraReportHospDohovirThemeModel>()
                .ForMember(dest => dest.PrintedPublicationHospDohovirThemeIds, opts => opts.MapFrom(src => src.PrintedPublicationHospDohovirTheme.Where(x => x.Checked).Select(x => x.Id)));

            CreateMap<CathedraReportModel, CathedraReportViewModel>();
        }
    }
}
