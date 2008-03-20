using StructureMap;

namespace Tarantino.Core.Deployer.Services
{
	[PluginFamily(Keys.Default)]
	public interface IRevisionNumberParser
	{
		int Parse(string output);
	}
}