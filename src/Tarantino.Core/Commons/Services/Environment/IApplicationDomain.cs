using StructureMap;

namespace Tarantino.Commons.Core.Services.Environment
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IApplicationDomain
	{
		string GetBaseFolder();
	}
}