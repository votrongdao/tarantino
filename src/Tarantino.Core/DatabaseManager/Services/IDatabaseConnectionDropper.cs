using Tarantino.DatabaseManager.Model;

namespace Tarantino.DatabaseManager.Services
{
	public interface IDatabaseConnectionDropper
	{
		void Drop(string databaseName, ConnectionSettings settings);
	}
}