using StructureMap;

namespace Tarantino.Commons.Core.Services.Daemon
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IServiceAgentAggregator
	{
		void ExecuteServiceAgentCycle();
	}
}