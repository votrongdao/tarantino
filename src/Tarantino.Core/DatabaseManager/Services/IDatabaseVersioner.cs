using StructureMap;
using Tarantino.DatabaseManager.Model;

namespace Tarantino.Core.DatabaseManager.Services
{
	[PluginFamily(Keys.Default)]
	public interface IDatabaseVersioner
	{
		void VersionDatabase(ConnectionSettings settings, ITaskObserver taskObserver);
	}
}