using StructureMap;
using Tarantino.Core.WebManagement.Model;

namespace Tarantino.Core.WebManagement.Services
{
	[PluginFamily(Keys.Default)]
	public interface IApplicationInstanceContext
	{
		ApplicationInstance GetCurrent();
	}
}