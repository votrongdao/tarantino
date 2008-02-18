using StructureMap;
using Tarantino.DatabaseManager.Model;

namespace Tarantino.Core.DatabaseManager.Services
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IConnectionStringGenerator
	{
		string GetConnectionString(ConnectionSettings settings);
	}
}