using Tarantino.DatabaseManager.NAntTasks.Domain;

namespace Tarantino.DatabaseManager.NAntTasks.Services
{
	public interface IChangeScriptExecutor
	{
		void Execute(string fullFilename, ConnectionSettings settings, ITaskObserver taskObserver);
	}
}