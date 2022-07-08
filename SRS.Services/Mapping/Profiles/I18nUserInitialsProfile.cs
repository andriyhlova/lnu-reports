using AutoMapper;
using SRS.Domain.Entities;
using SRS.Services.Models;

namespace SRS.Services.Mapping.Profiles
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Old name")]
    public class I18nUserInitialsProfile : Profile
    {
        public I18nUserInitialsProfile()
        {
            CreateMap<I18nUserInitials, I18nUserInitialsModel>().ReverseMap();
        }
    }
}
