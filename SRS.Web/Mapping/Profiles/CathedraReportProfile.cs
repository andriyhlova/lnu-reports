using AutoMapper;
using SRS.Services.Models.FilterModels;
using SRS.Web.Models.Reports;
using SRS.Web.Models.Shared;

namespace SRS.Services.Mapping.Profiles
{
    public class CathedraReportProfile : Profile
    {
        public CathedraReportProfile()
        {
            CreateMap<CathedraReportFilterViewModel, CathedraReportFilterModel>()
                .IncludeBase<DepartmentFilterViewModel, DepartmentFilterModel>();
        }
    }
}
