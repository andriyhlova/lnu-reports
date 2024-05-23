using System.Linq;
using AutoMapper;
using SRS.Domain.Entities;
using SRS.Domain.Enums;
using SRS.Services.Extensions;
using SRS.Services.Models.CsvModels;
using SRS.Services.Models.ReportModels;

namespace SRS.Services.Mapping.Profiles
{
    public class ReportProfile : Profile
    {
        public ReportProfile()
        {
            CreateMap<Report, BaseReportModel>()
                .ForMember(dest => dest.I18nUserInitials, opts => opts.MapFrom(src => src.User.I18nUserInitials));

            CreateMap<ReportPublicationsModel, Report>()
                .ForMember(dest => dest.PrintedPublication, opts => opts.MapFrom(src => src.PrintedPublicationIds.Select(x => new Publication { Id = x })))
                .ForMember(dest => dest.RecomendedPublication, opts => opts.MapFrom(src => src.RecomendedPublicationIds.Select(x => new Publication { Id = x })))
                .ForMember(dest => dest.AcceptedToPrintPublication, opts => opts.MapFrom(src => src.AcceptedToPrintPublicationIds.Select(x => new Publication { Id = x })))
            .ForMember(dest => dest.ApplicationsForInvention, opts => opts.MapFrom(src => src.ApplicationsForInventionIds.Select(x => new Publication { Id = x })))
                .ForMember(dest => dest.PatentsForInvention, opts => opts.MapFrom(src => src.PatentsForInventionIds.Select(x => new Publication { Id = x })));

            CreateMap<ReportThemeOfScientificWorkModel, ReportThemeOfScientificWork>().ReverseMap();

            CreateMap<ReportScientificWorkModel, Report>()
                .ForMember(dest => dest.StudentPublication, opts => opts.MapFrom(src => src.StudentPublicationIds.Select(x => new Publication { Id = x })))
                .ForMember(dest => dest.ThemeOfScientificWorks, opts => opts.MapFrom(src => (src.ThemeOfScientificWorks ?? new ReportThemeOfScientificWorkModel[0] { }).Union(src.Grants ?? new ReportThemeOfScientificWorkModel[0] { })));

            CreateMap<ReportOtherInfoModel, Report>();

            CreateMap<ReportFinalInfoModel, Report>();

            CreateMap<Report, ReportModel>()
                .IncludeBase<Report, BaseReportModel>()
                .ForMember(dest => dest.StudentPublicationIds, opts => opts.MapFrom(src => src.StudentPublication.Select(x => x.Id)))
                .ForMember(dest => dest.PrintedPublicationIds, opts => opts.MapFrom(src => src.PrintedPublication.Select(x => x.Id)))
                .ForMember(dest => dest.RecomendedPublicationIds, opts => opts.MapFrom(src => src.RecomendedPublication.Select(x => x.Id)))
                .ForMember(dest => dest.AcceptedToPrintPublicationIds, opts => opts.MapFrom(src => src.AcceptedToPrintPublication.Select(x => x.Id)))
                .ForMember(dest => dest.ApplicationsForInventionIds, opts => opts.MapFrom(src => src.ApplicationsForInvention.Select(x => x.Id)))
                .ForMember(dest => dest.PatentsForInventionIds, opts => opts.MapFrom(src => src.PatentsForInvention.Select(x => x.Id)));

            CreateMap<BaseReportModel, ReportCsvModel>()
                .ForMember(dest => dest.Date, opts => opts.MapFrom(src => src.Date.ToString()))
                .ForMember(dest => dest.State, opts => opts.MapFrom(src => src.State.GetDisplayName()))
                .ForMember(dest => dest.Employee, opts => opts.MapFrom(src => src.I18nUserInitials.Where(x => x.Language == Language.UA)
                .Select(x => x.LastName + " " + x.FirstName).FirstOrDefault()));
        }
    }
}
