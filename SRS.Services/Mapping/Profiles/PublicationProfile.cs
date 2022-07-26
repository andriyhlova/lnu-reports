using System.Linq;
using AutoMapper;
using SRS.Domain.Entities;
using SRS.Services.Models.PublicationModels;

namespace SRS.Services.Mapping.Profiles
{
    public class PublicationProfile : Profile
    {
        public PublicationProfile()
        {
            CreateMap<Publication, BasePublicationModel>()
                .ForMember(dest => dest.UserIds, opts => opts.MapFrom(src => src.User.Select(x => x.Id)));

            CreateMap<BasePublicationModel, Publication>()
                .ForMember(dest => dest.User, opts => opts.MapFrom(src => src.UserIds.Select(x => new ApplicationUser { Id = x })));

            CreateMap<Publication, PublicationModel>()
                .IncludeBase<Publication, BasePublicationModel>()
                .ForMember(dest => dest.Users, opts => opts.MapFrom(src => src.User))
                .ForMember(dest => dest.JournalName, opts => opts.MapFrom(src => src.GetJournalName()));

            CreateMap<PublicationModel, Publication>()
                .IncludeBase<BasePublicationModel, Publication>()
                .ForMember(dest => dest.User, opts => opts.MapFrom(src => src.Users));
        }
    }
}
