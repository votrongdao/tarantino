using Tarantino.DatabaseManager.NAntTasks.Domain;

namespace Tarantino.DatabaseManager.NAntTasks.Services
{
	public interface IDatabaseVersioner
	{
		void VersionDatabase(ConnectionSettings settings, ITaskObserver taskObserver, string databaseVersionPropertyName);
	}
}