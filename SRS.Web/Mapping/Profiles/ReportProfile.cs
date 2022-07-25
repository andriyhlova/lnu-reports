using AutoMapper;
using SRS.Services.Models.FilterModels;
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
        }
    }
}
