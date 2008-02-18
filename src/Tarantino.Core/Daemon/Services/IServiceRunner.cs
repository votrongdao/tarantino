using StructureMap;

namespace Tarantino.Core.Daemon.Services
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IServiceRunner
	{
		void Start();
		void Stop();
		void RunOneCycle();
	}
}