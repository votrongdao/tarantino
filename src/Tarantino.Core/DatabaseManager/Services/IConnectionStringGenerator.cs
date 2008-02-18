using Tarantino.DatabaseManager.Model;

namespace Tarantino.DatabaseManager.Services
{
	public interface IConnectionStringGenerator
	{
		string GetConnectionString(ConnectionSettings settings);
	}
}