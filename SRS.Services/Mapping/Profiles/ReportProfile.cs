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
        }
    }
}
