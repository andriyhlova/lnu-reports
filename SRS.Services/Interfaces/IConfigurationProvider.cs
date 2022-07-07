using System.Collections.Specialized;

namespace SRS.Services.Interfaces
{
    public interface IConfigurationProvider
    {
        string Get(string key);
    }
}
