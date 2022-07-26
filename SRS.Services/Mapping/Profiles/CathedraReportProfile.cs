using AutoMapper;
using SRS.Domain.Entities;
using SRS.Services.Models.CathedraReportModels;

namespace SRS.Services.Mapping.Profiles
{
    public class CathedraReportProfile : Profile
    {
        public CathedraReportProfile()
        {
            CreateMap<CathedraReport, BaseCathedraReportModel>()
                .ForMember(dest => dest.CathedraName, opts => opts.MapFrom(src => src.User.Cathedra.Name))
                .ForMember(dest => dest.I18nUserInitials, opts => opts.MapFrom(src => src.User.I18nUserInitials));
        }
    }
}
