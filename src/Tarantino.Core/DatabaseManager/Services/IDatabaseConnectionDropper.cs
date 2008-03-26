using StructureMap;
using Tarantino.DatabaseManager.Model;

namespace Tarantino.Core.DatabaseManager.Services
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IDatabaseConnectionDropper
	{
		void Drop(ConnectionSettings settings, ITaskObserver taskObserver);
	}
}