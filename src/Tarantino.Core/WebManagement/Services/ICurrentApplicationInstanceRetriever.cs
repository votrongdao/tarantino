using StructureMap;
using Tarantino.Core.WebManagement.Model;

namespace Tarantino.Core.WebManagement.Services
{
	[PluginFamily(ServiceKeys.Default)]
	public interface ICurrentApplicationInstanceRetriever
	{
		ApplicationInstance GetApplicationInstance();
	}
}