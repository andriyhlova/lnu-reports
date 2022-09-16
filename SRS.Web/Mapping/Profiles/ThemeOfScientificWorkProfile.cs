﻿using System.Web.Mvc;
using AutoMapper;
using SRS.Domain.Enums;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.ThemeOfScientificWorkModels;
using SRS.Web.Models.Shared;
using SRS.Web.Models.ThemeOfScientificWorks;

namespace SRS.Services.Mapping.Profiles
{
    public class ThemeOfScientificWorkProfile : Profile
    {
        public ThemeOfScientificWorkProfile()
        {
            CreateMap<ThemeOfScientificWorkModel, SelectListItem>()
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opts => opts.MapFrom(src => GetThemeOfScientificWorkText(src)));

            CreateMap<ThemeOfScientificWorkFilterViewModel, ThemeOfScientificWorkFilterModel>()
                .IncludeBase<BaseFilterViewModel, BaseFilterModel>();
        }

        private string GetThemeOfScientificWorkText(ThemeOfScientificWorkModel src)
        {
            if (src.Financial == Financial.Budget)
            {
                return $"{src.Code} {src.Value}";
            }

            if (src.Financial == Financial.InWorkTime)
            {
                return $"{src.ScientificHead} {src.Value}";
            }

            return src.Value;
        }
    }
}
