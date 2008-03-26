using StructureMap;

namespace Tarantino.Core.Deployer.Services
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IRevisionNumberParser
	{
		int Parse(string output);
	}
}