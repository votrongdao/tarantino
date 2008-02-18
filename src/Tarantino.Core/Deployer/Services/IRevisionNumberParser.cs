using Tarantino.Commons.Core;
using StructureMap;

namespace Tarantino.Deployer.Core.Services
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IRevisionNumberParser
	{
		int Parse(string output);
	}
}