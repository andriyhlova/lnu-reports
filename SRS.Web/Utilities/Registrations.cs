using AutoMapper;
using Ninject;
using Ninject.Modules;
using SRS.Services.Utilities;

namespace SRS.Web.Utilities
{
    public class Registrations : NinjectModule
    {
        public override void Load()
        {
            var mapperConfiguration = CreateConfiguration();
            Bind<MapperConfiguration>().ToConstant(mapperConfiguration).InSingletonScope();

            Bind<IMapper>().ToMethod(ctx =>
                 new Mapper(mapperConfiguration, type => ctx.Kernel.Get(type)));
        }

        private MapperConfiguration CreateConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(GetType().Assembly);
                cfg.AddProfiles(typeof(ServicesRegistrations).Assembly);
            });
        }
    }
}
