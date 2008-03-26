using StructureMap;

namespace Tarantino.Core.Commons.Services.Environment
{
	[PluginFamily(ServiceKeys.Default)]
	public interface ISystemEnvironment
	{
		string GetMachineName();
	}
}