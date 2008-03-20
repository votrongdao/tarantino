using StructureMap;

namespace Tarantino.Core.Commons.Services.Environment
{
	[PluginFamily(Keys.Default)]
	public interface ISystemEnvironment
	{
		string GetMachineName();
	}
}