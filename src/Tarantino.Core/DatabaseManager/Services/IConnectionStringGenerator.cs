using StructureMap;
using Tarantino.DatabaseManager.Model;

namespace Tarantino.Core.DatabaseManager.Services
{
	[PluginFamily(Keys.Default)]
	public interface IConnectionStringGenerator
	{
		string GetConnectionString(ConnectionSettings settings, bool includeDatabaseName);
	}
}