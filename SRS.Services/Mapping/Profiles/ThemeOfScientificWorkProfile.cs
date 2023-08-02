using AutoMapper;
using SRS.Domain.Entities;
using SRS.Services.Models.ThemeOfScientificWorkModels;
using System;
using System.Linq;

namespace SRS.Services.Mapping.Profiles
{
    public class ThemeOfScientificWorkProfile : Profile
    {
        public ThemeOfScientificWorkProfile()
        {
            CreateMap<ThemeOfScientificWork, ThemeOfScientificWorkModel>();
            CreateMap<ThemeOfScientificWorkModel, ThemeOfScientificWork>()
                .ForMember(dest => dest.IsActive, opts => opts.Ignore());
            CreateMap<ThemeOfScientificWorkFinancial, ThemeOfScientificWorkFinancialModel>().ReverseMap();

            CreateMap<ThemeOfScientificWorkCathedra, ThemeOfScientificWorkCathedraModel>()
                .ForMember(dest => dest.FacultyName, opts => opts.MapFrom(src => src.Cathedra.Faculty.Name));

            CreateMap<ThemeOfScientificWorkCathedraModel, ThemeOfScientificWorkCathedra>();

            CreateMap<ThemeOfScientificWork, BaseThemeOfScientificWorkModel>().ReverseMap();
            CreateMap<ThemeOfScientificWork, BaseThemeOfScientificWorkWithFinancialsModel>().ReverseMap();

            CreateMap<ThemeOfScientificWork, CathedraReportThemeOfScientificWorkModel>()
                .ForMember(
                dest => dest.ReportThemeOfScientificWork,
                opts => opts.ResolveUsing((src, dest, destMember, resContext) => src.Reports
                            .OrderByDescending(x => x.Id)
                            .FirstOrDefault(r => r.Report.Date.HasValue &&
                                r.Report.Date.Value.Year == ((DateTime)resContext.Items["date"]).Year &&
                                r.Report.UserId == src.SupervisorId)));
        }
    }
}
