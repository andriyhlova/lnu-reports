using System.Linq;
using AutoMapper;
using SRS.Domain.Entities;
using SRS.Services.Extensions;
using SRS.Services.Models.CsvModels;
using SRS.Services.Models.PublicationModels;
using SRS.Services.Utilities;

namespace SRS.Services.Mapping.Profiles
{
    public class PublicationProfile : Profile
    {
        public PublicationProfile()
        {
            CreateMap<Publication, BasePublicationModel>()
                .ForMember(dest => dest.UserIds, opts => opts.MapFrom(src => src.User.Select(x => x.Id)))
                .ForMember(dest => dest.JournalName, opts => opts.MapFrom(src => src.GetJournalName(false)));

            CreateMap<BasePublicationModel, Publication>()
                .ForMember(dest => dest.User, opts => opts.MapFrom(src => src.UserIds.Select(x => new ApplicationUser { Id = x })));

            CreateMap<Publication, PublicationModel>()
                .IncludeBase<Publication, BasePublicationModel>()
                .ForMember(dest => dest.Users, opts => opts.MapFrom(src => src.User));

            CreateMap<PublicationModel, Publication>()
                .IncludeBase<BasePublicationModel, Publication>()
                .ForMember(dest => dest.User, opts => opts.MapFrom(src => src.Users));

            CreateMap<BasePublicationModel, PublicationCsvModel>()
                .ForMember(dest => dest.Date, opts => opts.MapFrom(src => src.Date.ToString("yyyy")))
                .ForMember(dest => dest.PublicationType, opts => opts.MapFrom(src => src.PublicationType.GetDisplayName()))
                .ForMember(dest => dest.JournalOrChapterMonographyOrConference, opts => opts.MapFrom(src => StringUtilities.JoinNotNullOrWhitespace(" / ", src.JournalName, src.ChapterMonographyName, src.ConferenceName)))
                .ForMember(dest => dest.Authors, opts => opts.MapFrom(src => src.AuthorsOrder));
        }
    }
}
