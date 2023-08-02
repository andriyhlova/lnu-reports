using AutoMapper;
using SRS.Services.Models.CathedraReportModels;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.ReportModels;
using SRS.Web.Models.CathedraReports;
using SRS.Web.Models.Reports;
using SRS.Web.Models.Shared;
using System.Linq;

namespace SRS.Web.Mapping.Profiles
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

            CreateMap<CathedraReportPublicationsViewModel, CathedraReportPublicationsModel>()
                .ForMember(dest => dest.PublicationsIds, opts => opts.MapFrom(src => src.Publications.Where(x => x.Checked).Select(x => x.Id)))
                .ForMember(dest => dest.ApplicationsForInventionIds, opts => opts.MapFrom(src => src.ApplicationsForInvention.Where(x => x.Checked).Select(x => x.Id)))
                .ForMember(dest => dest.PatentsForInventionIds, opts => opts.MapFrom(src => src.PatentsForInvention.Where(x => x.Checked).Select(x => x.Id)));

            CreateMap<CathedraReportGrantsViewModel, CathedraReportGrantsModel>()
               .ForMember(dest => dest.GrantsIds, opts => opts.MapFrom(src => src.Grants.Where(x => x.Checked).Select(x => x.Id)));

            CreateMap<ReportPublicationsViewModel, ReportPublicationsModel>()
                .ForMember(dest => dest.PrintedPublicationIds, opts => opts.MapFrom(src => src.PrintedPublication.Where(x => x.Checked).Select(x => x.Id)))
                .ForMember(dest => dest.RecomendedPublicationIds, opts => opts.MapFrom(src => src.RecomendedPublication.Where(x => x.Checked).Select(x => x.Id)))
                .ForMember(dest => dest.AcceptedToPrintPublicationIds, opts => opts.MapFrom(src => src.AcceptedToPrintPublication.Where(x => x.Checked).Select(x => x.Id)))
                .ForMember(dest => dest.ApplicationsForInventionIds, opts => opts.MapFrom(src => src.ApplicationsForInvention.Where(x => x.Checked).Select(x => x.Id)))
                .ForMember(dest => dest.PatentsForInventionIds, opts => opts.MapFrom(src => src.PatentsForInvention.Where(x => x.Checked).Select(x => x.Id)));

            CreateMap<CathedraReportModel, CathedraReportViewModel>();
        }
    }
}
