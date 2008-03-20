using StructureMap;
using Tarantino.DatabaseManager.Model;

namespace Tarantino.Core.DatabaseManager.Services
{
	[PluginFamily(Keys.Default)]
	public interface ISchemaInitializer
	{
		void EnsureSchemaCreated(ConnectionSettings settings);
	}
}