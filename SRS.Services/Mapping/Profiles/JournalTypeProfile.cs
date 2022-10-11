﻿using AutoMapper;
using SRS.Domain.Entities;
using SRS.Services.Models.JournalModels;

namespace SRS.Services.Mapping.Profiles
{
    public class JournalTypeProfile : Profile
    {
        public JournalTypeProfile()
        {
            CreateMap<JournalType, JournalTypeModel>().ReverseMap();
        }
    }
}