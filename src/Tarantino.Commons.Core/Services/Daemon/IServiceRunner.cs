using StructureMap;

namespace Tarantino.Commons.Core.Services.Daemon
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IServiceRunner
	{
		void Start();
		void Stop();
		void RunOneCycle();
	}
}