using StructureMap;

namespace Tarantino.Core.Daemon.Services
{
	[PluginFamily()]
	public interface IServiceAgent
	{
		void Run();
		
		string AgentName { get; }
	}
}