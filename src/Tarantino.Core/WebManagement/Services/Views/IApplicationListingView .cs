using StructureMap;

namespace Tarantino.Core.WebManagement.Services.Views
{
    [PluginFamily(Keys.Default)]
    public interface IApplicationListingView 
    {
        void Render();
    }
}