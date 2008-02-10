using Tarantino.DatabaseManager.NAntTasks.Domain;

namespace Tarantino.DatabaseManager.NAntTasks.Services
{
	public interface IConnectionStringGenerator
	{
		string GetConnectionString(ConnectionSettings settings);
	}
}