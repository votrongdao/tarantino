using Tarantino.DatabaseManager.Model;

namespace Tarantino.DatabaseManager.Services
{
	public interface ISchemaInitializer
	{
		void EnsureSchemaCreated(ConnectionSettings settings);
	}
}