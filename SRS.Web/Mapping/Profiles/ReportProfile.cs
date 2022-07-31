using AutoMapper;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.ReportModels;
using SRS.Web.Models.Reports;
using SRS.Web.Models.Shared;
using System.Linq;

namespace SRS.Services.Mapping.Profiles
{
    public class ReportProfile : Profile
    {
        public ReportProfile()
        {
            CreateMap<ReportFilterViewModel, ReportFilterModel>()
                .IncludeBase<DepartmentFilterViewModel, DepartmentFilterModel>();

            CreateMap<ReportPublicationsViewModel, ReportPublicationsModel>()
                .ForMember(dest => dest.PrintedPublicationIds, opts => opts.MapFrom(src => src.PrintedPublication.Where(x=>x.Checked).Select(x=>x.Id)))
                .ForMember(dest => dest.RecomendedPublicationIds, opts => opts.MapFrom(src => src.RecomendedPublication.Where(x => x.Checked).Select(x => x.Id)))
                .ForMember(dest => dest.AcceptedToPrintPublicationIds, opts => opts.MapFrom(src => src.AcceptedToPrintPublication.Where(x => x.Checked).Select(x => x.Id)));

            CreateMap<ReportModel, ReportViewModel>();
        }
    }
}
