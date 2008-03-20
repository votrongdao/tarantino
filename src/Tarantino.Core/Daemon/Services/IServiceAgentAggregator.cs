using StructureMap;

namespace Tarantino.Core.Daemon.Services
{
	[PluginFamily(Keys.Default)]
	public interface IServiceAgentAggregator
	{
		void ExecuteServiceAgentCycle();
	}
}