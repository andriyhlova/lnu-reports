using System.Configuration;
using SRS.Services.Interfaces;

namespace SRS.Services.Implementations
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public string Get(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
