using StructureMap;

namespace Tarantino.Core.Daemon.Services
{
	[PluginFamily(Keys.Default)]
	public interface IServiceRunner
	{
		void Start();
		void Stop();
		void RunOneCycle();
	}
}