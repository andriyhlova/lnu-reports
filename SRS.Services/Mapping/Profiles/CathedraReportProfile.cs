using System.Linq;
using AutoMapper;
using SRS.Domain.Entities;
using SRS.Services.Models.CathedraReportModels;
using SRS.Services.Models.ReportModels;

namespace SRS.Services.Mapping.Profiles
{
    public class CathedraReportProfile : Profile
    {
        public CathedraReportProfile()
        {
            CreateMap<CathedraReport, BaseCathedraReportModel>()
                .ForMember(dest => dest.CathedraName, opts => opts.MapFrom(src => src.User.Cathedra.Name))
                .ForMember(dest => dest.I18nUserInitials, opts => opts.MapFrom(src => src.User.I18nUserInitials));

            CreateMap<CathedraReportBudgetThemeModel, CathedraReport>()
                .ForMember(dest => dest.PrintedPublicationBudgetTheme, opts => opts.MapFrom(src => src.PrintedPublicationBudgetThemeIds.Select(x => new Publication { Id = x })));

            CreateMap<CathedraReportInTimeThemeModel, CathedraReport>()
                .ForMember(dest => dest.PrintedPublicationThemeInWorkTime, opts => opts.MapFrom(src => src.PrintedPublicationThemeInWorkTimeIds.Select(x => new Publication { Id = x })));

            CreateMap<CathedraReportHospDohovirThemeModel, CathedraReport>()
                .ForMember(dest => dest.PrintedPublicationHospDohovirTheme, opts => opts.MapFrom(src => src.PrintedPublicationHospDohovirThemeIds.Select(x => new Publication { Id = x })));

            CreateMap<CathedraReportOtherInfoModel, CathedraReport>();

            CreateMap<CathedraReportFinalInfoModel, CathedraReport>();

            CreateMap<CathedraReport, CathedraReportModel>()
                .IncludeBase<CathedraReport, BaseCathedraReportModel>()
                .ForMember(dest => dest.CathedraId, opts => opts.MapFrom(src => src.User.CathedraId))
                .ForMember(dest => dest.PrintedPublicationBudgetThemeIds, opts => opts.MapFrom(src => src.PrintedPublicationBudgetTheme.Select(x => x.Id)))
                .ForMember(dest => dest.PrintedPublicationThemeInWorkTimeIds, opts => opts.MapFrom(src => src.PrintedPublicationThemeInWorkTime.Select(x => x.Id)))
                .ForMember(dest => dest.PrintedPublicationHospDohovirThemeIds, opts => opts.MapFrom(src => src.PrintedPublicationHospDohovirTheme.Select(x => x.Id)));
        }
    }
}
