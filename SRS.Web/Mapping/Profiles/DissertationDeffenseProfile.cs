using AutoMapper;
using SRS.Services.Models.FilterModels;
using SRS.Web.Models.DissertationDefense;
using SRS.Web.Models.Shared;
using SRS.Web.Models.ThemeOfScientificWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SRS.Web.Mapping.Profiles
{
    public class DissertationDeffenseProfile : Profile
    {
        public DissertationDeffenseProfile()
        {
            CreateMap<DissertationDefenseFilterViewModel, DissertationDefenseFilterModel>()
                .IncludeBase<BaseFilterViewModel, BaseFilterModel>();
        }
    }
}