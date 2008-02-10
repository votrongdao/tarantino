using Tarantino.DatabaseManager.NAntTasks.Domain;

namespace Tarantino.DatabaseManager.NAntTasks.Services
{
	public interface IDatabaseConnectionDropper
	{
		void Drop(string databaseName, ConnectionSettings settings);
	}
}