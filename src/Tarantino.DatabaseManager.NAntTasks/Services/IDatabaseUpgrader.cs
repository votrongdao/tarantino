using Tarantino.DatabaseManager.NAntTasks.Services.Impl;

namespace Tarantino.DatabaseManager.NAntTasks.Services
{
	public interface IDatabaseUpgrader
	{
		void Upgrade(string scriptDirectory, string server, string database, bool integrated, string username, string password, DatabaseAction action, ITaskObserver taskObserver, string databaseVersionPropertyName);
	}
}