using Tarantino.Core.DatabaseManager.Services.Impl;

namespace Tarantino.Core.DatabaseManager.Services
{
	public interface ISqlDatabaseManager
	{
		void Upgrade(string scriptDirectory, string server, string database, bool integrated, string username, string password,
		             RequestedDatabaseAction requestedAction, ITaskObserver taskObserver);
	}
}