using System.Linq;
using AutoMapper;
using SRS.Domain.Enums;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.ReportModels;
using SRS.Web.Models.Reports;
using SRS.Web.Models.Shared;

namespace SRS.Services.Mapping.Profiles
{
    public class ReportProfile : Profile
    {
        public ReportProfile()
        {
            CreateMap<ReportFilterViewModel, ReportFilterModel>()
                .IncludeBase<DepartmentFilterViewModel, DepartmentFilterModel>();

            CreateMap<ReportPublicationsViewModel, ReportPublicationsModel>()
                .ForMember(dest => dest.PrintedPublicationIds, opts => opts.MapFrom(src => src.PrintedPublication.Where(x => x.Checked).Select(x => x.Id)))
                .ForMember(dest => dest.RecomendedPublicationIds, opts => opts.MapFrom(src => src.RecomendedPublication.Where(x => x.Checked).Select(x => x.Id)))
                .ForMember(dest => dest.AcceptedToPrintPublicationIds, opts => opts.MapFrom(src => src.AcceptedToPrintPublication.Where(x => x.Checked).Select(x => x.Id)))
                .ForMember(dest => dest.ApplicationsForInventionIds, opts => opts.MapFrom(src => src.ApplicationsForInvention.Where(x => x.Checked).Select(x => x.Id)))
                .ForMember(dest => dest.PatentsForInventionIds, opts => opts.MapFrom(src => src.PatentsForInvention.Where(x => x.Checked).Select(x => x.Id)));

            CreateMap<ReportModel, ReportViewModel>()
                .ForMember(dest => dest.ThemeOfScientificWorks, opts => opts.MapFrom(src => src.ThemeOfScientificWorks.Where(x => x.Financial != Financial.InternationalGrant)))
                .ForMember(dest => dest.Grants, opts => opts.MapFrom(src => src.ThemeOfScientificWorks.Where(x => x.Financial == Financial.InternationalGrant)));
        }
    }
}
