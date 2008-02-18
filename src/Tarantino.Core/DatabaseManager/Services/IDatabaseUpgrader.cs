using StructureMap;
using Tarantino.Core;
using Tarantino.DatabaseManager.Services.Impl;

namespace Tarantino.DatabaseManager.Services
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IDatabaseUpgrader
	{
		void Upgrade(string scriptDirectory, string server, string database, bool integrated, string username, string password, DatabaseAction action, ITaskObserver taskObserver, string databaseVersionPropertyName);
	}
}