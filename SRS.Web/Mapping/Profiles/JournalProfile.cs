using AutoMapper;
using SRS.Services.Models.FilterModels;
using SRS.Web.Models.Journals;
using SRS.Web.Models.Shared;

namespace SRS.Web.Mapping.Profiles
{
    public class JournalProfile : Profile
    {
        public JournalProfile()
        {
            CreateMap<JournalFilterViewModel, JournalFilterModel>()
                .IncludeBase<BaseFilterViewModel, BaseFilterModel>();
        }
    }
}
