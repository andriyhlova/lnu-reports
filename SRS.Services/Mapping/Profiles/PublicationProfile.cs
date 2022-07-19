using System.Linq;
using AutoMapper;
using SRS.Domain.Entities;
using SRS.Services.Models;

namespace SRS.Services.Mapping.Profiles
{
    public class PublicationProfile : Profile
    {
        public PublicationProfile()
        {
            CreateMap<Publication, BasePublicationModel>()
                .ForMember(dest => dest.UserIds, opts => opts.MapFrom(src => src.User.Select(x => x.Id)));

            CreateMap<Publication, PublicationModel>()
                .IncludeBase<Publication, BasePublicationModel>();
        }
    }
}
