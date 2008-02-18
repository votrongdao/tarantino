using Tarantino.DatabaseManager.NAntTasks.Domain;

namespace Tarantino.DatabaseManager.NAntTasks.Services
{
	public interface ISchemaInitializer
	{
		void EnsureSchemaCreated(ConnectionSettings settings);
	}
}