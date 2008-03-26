using StructureMap;

namespace Tarantino.Core.WebManagement.Services
{
    [PluginFamily(ServiceKeys.Default)]
    public interface IApplicationListingManager 
    {
        void HandleRequest();
    }
}