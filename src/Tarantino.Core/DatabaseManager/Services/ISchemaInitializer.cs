
using Tarantino.DatabaseManager.Model;

namespace Tarantino.Core.DatabaseManager.Services
{
	
	public interface ISchemaInitializer
	{
		void EnsureSchemaCreated(ConnectionSettings settings);
	}
}