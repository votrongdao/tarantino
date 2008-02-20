using StructureMap;
using Tarantino.DatabaseManager.Model;

namespace Tarantino.Core.DatabaseManager.Services
{
	[PluginFamily()]
	public interface IDatabaseActionExecutor
	{
		void Execute(string scriptFolder, ConnectionSettings settings, ITaskObserver taskObserver);
	}
}