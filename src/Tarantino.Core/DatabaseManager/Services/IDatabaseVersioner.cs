using Tarantino.DatabaseManager.Model;

namespace Tarantino.DatabaseManager.Services
{
	public interface IDatabaseVersioner
	{
		void VersionDatabase(ConnectionSettings settings, ITaskObserver taskObserver, string databaseVersionPropertyName);
	}
}