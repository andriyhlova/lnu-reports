using AutoMapper;
using SRS.Domain.Entities;
using SRS.Domain.Enums;
using SRS.Services.Extensions;
using SRS.Services.Models.CsvModels;
using SRS.Services.Models.DepartmentReportModels;
using System.Linq;

namespace SRS.Services.Mapping.Profiles
{
    public class CathedraReportProfile : Profile
    {
        public CathedraReportProfile()
        {
            CreateMap<CathedraReport, BaseDepartmentReportModel>()
                .ForMember(dest => dest.DepartmentName, opts => opts.MapFrom(src => src.User.Cathedra.Name))
                .ForMember(dest => dest.I18nUserInitials, opts => opts.MapFrom(src => src.User.I18nUserInitials));

            CreateMap<DepartmentReportAchievementSchoolModel, CathedraReport>();

            CreateMap<DepartmentReportPublicationsModel, CathedraReport>()
                .ForMember(dest => dest.Publications, opts => opts.MapFrom(src => src.PublicationsIds.Select(x => new Publication { Id = x })))
                .ForMember(dest => dest.ApplicationsForInvention, opts => opts.MapFrom(src => src.ApplicationsForInventionIds.Select(x => new Publication { Id = x })))
                .ForMember(dest => dest.PatentsForInvention, opts => opts.MapFrom(src => src.PatentsForInventionIds.Select(x => new Publication { Id = x })));

            CreateMap<DepartmentReportGrantsModel, CathedraReport>()
                .ForMember(dest => dest.Grants, opts => opts.MapFrom(src => src.GrantsIds.Select(x => new ThemeOfScientificWork { Id = x })));

            CreateMap<DepartmentReportOtherInfoModel, CathedraReport>();

            CreateMap<DepartmentReportFinalInfoModel, CathedraReport>();

            CreateMap<CathedraReport, DepartmentReportModel>()
                .IncludeBase<CathedraReport, BaseDepartmentReportModel>()
                .ForMember(dest => dest.DepartmentId, opts => opts.MapFrom(src => src.User.CathedraId))
                .ForMember(dest => dest.PublicationsIds, opts => opts.MapFrom(src => src.Publications.Select(x => x.Id)))
                .ForMember(dest => dest.ApplicationsForInventionIds, opts => opts.MapFrom(src => src.ApplicationsForInvention.Select(x => x.Id)))
                .ForMember(dest => dest.PatentsForInventionIds, opts => opts.MapFrom(src => src.PatentsForInvention.Select(x => x.Id)))
                .ForMember(dest => dest.GrantsIds, opts => opts.MapFrom(src => src.Grants.Select(x => x.Id)));

            CreateMap<BaseDepartmentReportModel, CathedraReportCsvModel>()
                .ForMember(dest => dest.CathedraName, opts => opts.MapFrom(src => src.DepartmentName))
                .ForMember(dest => dest.Date, opts => opts.MapFrom(src => src.Date.ToString()))
                .ForMember(dest => dest.State, opts => opts.MapFrom(src => src.State.GetDisplayName()))
                .ForMember(dest => dest.Head, opts => opts.MapFrom(src => src.I18nUserInitials.Where(x => x.Language == Language.UA)
                .Select(x => x.LastName + " " + x.FirstName).FirstOrDefault()));
        }
    }
}
