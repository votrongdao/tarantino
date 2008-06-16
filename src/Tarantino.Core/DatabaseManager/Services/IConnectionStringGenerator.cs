
using Tarantino.DatabaseManager.Model;

namespace Tarantino.Core.DatabaseManager.Services
{
	
	public interface IConnectionStringGenerator
	{
		string GetConnectionString(ConnectionSettings settings, bool includeDatabaseName);
	}
}