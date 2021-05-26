using AutoMapper;
using ScientificReport.DAL;
using ScientificReport.DAL.DTO;
using ScientificReport.DAL.Models;
using UserManagement.Models;

namespace UserManagement
{

    public class AutoMapperConfig
    {
        private static MapperConfiguration _instance;
        public static MapperConfiguration Instance => _instance ?? (_instance = CreateConfiguration());
        private static MapperConfiguration CreateConfiguration()
        {
            var res = new MapperConfiguration(config =>
            {
                config.CreateMap<AcademicStatus, AcademicStatusViewModel>().ReverseMap();
                config.CreateMap<RegisterViewModel, RegisterDTO>().ReverseMap();
                config.CreateMap<UpdateViewModel, UpdateDTO>().ReverseMap();
            });
            return res;
        }
    }

}