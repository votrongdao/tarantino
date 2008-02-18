using Tarantino.DatabaseManager.Model;

namespace Tarantino.DatabaseManager.Services
{
	public interface IChangeScriptExecutor
	{
		void Execute(string fullFilename, ConnectionSettings settings, ITaskObserver taskObserver);
	}
}