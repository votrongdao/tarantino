using StructureMap;

namespace Tarantino.Core.Daemon.Services
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IServiceAgentAggregator
	{
		void ExecuteServiceAgentCycle();
	}
}