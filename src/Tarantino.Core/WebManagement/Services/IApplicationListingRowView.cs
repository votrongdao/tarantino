using StructureMap;
using Tarantino.Core.WebManagement.Model;

namespace Tarantino.Core.WebManagement.Services
{
    [PluginFamily(ServiceKeys.Default)]
    public interface IApplicationListingRowView
    {
        string BuildFirstRowHtml(ApplicationInstance applicationInstance, int instanceCount);
        string BuildMRowHtml(ApplicationInstance applicationInstance);
    }
}