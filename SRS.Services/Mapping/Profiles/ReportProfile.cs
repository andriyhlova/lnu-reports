using System.Linq;
using AutoMapper;
using SRS.Domain.Entities;
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
                .ForMember(dest => dest.AcceptedToPrintPublication, opts => opts.MapFrom(src => src.AcceptedToPrintPublicationIds.Select(x => new Publication { Id = x })));

            CreateMap<ReportScientificWorkModel, Report>()
                .ForMember(dest => dest.ThemeOfScientificWorks, opts => opts.MapFrom(src => src.ThemeOfScientificWorkIds.Select(x => new ThemeOfScientificWork { Id = x })));

            CreateMap<ReportOtherInfoModel, Report>();

            CreateMap<ReportFinalInfoModel, Report>();

            CreateMap<Report, ReportModel>()
                .IncludeBase<Report, BaseReportModel>()
                .ForMember(dest => dest.PrintedPublicationIds, opts => opts.MapFrom(src => src.PrintedPublication.Select(x => x.Id)))
                .ForMember(dest => dest.RecomendedPublicationIds, opts => opts.MapFrom(src => src.RecomendedPublication.Select(x => x.Id)))
                .ForMember(dest => dest.AcceptedToPrintPublicationIds, opts => opts.MapFrom(src => src.AcceptedToPrintPublication.Select(x => x.Id)));
        }
    }
}
