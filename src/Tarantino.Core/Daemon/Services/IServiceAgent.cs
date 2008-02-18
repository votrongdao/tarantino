using StructureMap;

namespace Tarantino.Commons.Core.Services.Daemon
{
	[PluginFamily()]
	public interface IServiceAgent
	{
		void Run();
		
		string AgentName { get; }
	}
}