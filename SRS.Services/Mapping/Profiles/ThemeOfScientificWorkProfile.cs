﻿using AutoMapper;
using SRS.Domain.Entities;
using SRS.Services.Extensions;
using SRS.Services.Models.CsvModels;
using SRS.Services.Models.ThemeOfScientificWorkModels;
using System.Linq;

namespace SRS.Services.Mapping.Profiles
{
    public class ThemeOfScientificWorkProfile : Profile
    {
        public ThemeOfScientificWorkProfile()
        {
            CreateMap<BaseThemeOfScientificWorkWithFinancialsModel, ThemeOfScientificWorkCsvModel>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Value))
                .ForMember(dest => dest.Faculties, opts => opts.MapFrom(src => string.Join(", ", src.ThemeOfScientificWorkCathedras
                .OrderBy(x => x.FacultyName).Select(x => x.FacultyName).Distinct())))
                .ForMember(dest => dest.Cathedras, opts => opts.MapFrom(src => string.Join(", ", src.ThemeOfScientificWorkCathedras
                .OrderBy(x => x.CathedraName).Select(x => x.CathedraName).Distinct())))
                .ForMember(dest => dest.PeriodFrom, opts => opts.MapFrom(src => src.PeriodFrom.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.PeriodTo, opts => opts.MapFrom(src => src.PeriodTo.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.Financial, opts => opts.MapFrom(src => src.Financial.GetDisplayName()))
                .ForMember(dest => dest.SubCategory, opts => opts.MapFrom(src => src.SubCategory != null ? src.SubCategory.GetDisplayName() : string.Empty))
                .ForMember(dest => dest.Currency, opts => opts.MapFrom(src => src.Currency != null ? src.Currency.GetDisplayName() : string.Empty))
                .ForMember(dest => dest.Amount, opts => opts.MapFrom(src => string
                .Join(", ", src.ThemeOfScientificWorkFinancials.Select(x => x.Year.ToString() + "р. - " + x.Amount.ToString() + "грн. ")) + "Сума - " +
                src.ThemeOfScientificWorkFinancials.Sum(x => x.Amount).ToString() + "грн."))
                .ForMember(dest => dest.SupervisorsDescription, opts => opts.MapFrom(src => string
                .Join(", ", src.ThemeOfScientificWorkSupervisors.Select(x => x.SupervisorName))));

            CreateMap<ThemeOfScientificWork, BaseThemeOfScientificWorkModel>()
                .ForMember(dest => dest.ScientificHead, opts => opts.MapFrom(src => string
                .Join(", ", src.ThemeOfScientificWorkSupervisors.Select(x => x.GetSupervisor()))))
                .ForMember(dest => dest.SupervisorId, opts => opts.MapFrom(src => string
                .Join(",", src.ThemeOfScientificWorkSupervisors.Select(x => x.SupervisorId))));

            CreateMap<ThemeOfScientificWork, ThemeOfScientificWorkModel>()
                .IncludeBase<ThemeOfScientificWork, BaseThemeOfScientificWorkModel>()
                .IncludeBase<ThemeOfScientificWork, BaseThemeOfScientificWorkWithFinancialsModel>();

            CreateMap<ThemeOfScientificWorkModel, ThemeOfScientificWork>()
                .ForMember(dest => dest.IsActive, opts => opts.Ignore());

            CreateMap<ThemeOfScientificWorkFinancial, ThemeOfScientificWorkFinancialModel>().ReverseMap();

            CreateMap<ThemeOfScientificWorkCathedra, ThemeOfScientificWorkCathedraModel>()
                .ForMember(dest => dest.FacultyName, opts => opts.MapFrom(src => src.Cathedra.Faculty.Name));

            CreateMap<ThemeOfScientificWorkCathedraModel, ThemeOfScientificWorkCathedra>();

            CreateMap<ThemeOfScientificWork, CathedraReportThemeOfScientificWorkModel>();

            CreateMap<BaseThemeOfScientificWorkWithFinancialsModel, ThemeOfScientificWork>().ReverseMap();

            CreateMap<ThemeOfScientificWorkSupervisor, ThemeOfScientificWorkSupervisorModel>()
                .ForMember(dest => dest.SupervisorName, opts => opts.MapFrom(src => src.GetSupervisor()));

            CreateMap<ThemeOfScientificWorkSupervisorModel, ThemeOfScientificWorkSupervisor>();
        }
    }
}
