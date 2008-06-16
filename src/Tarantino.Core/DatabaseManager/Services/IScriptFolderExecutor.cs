
using Tarantino.DatabaseManager.Model;

namespace Tarantino.Core.DatabaseManager.Services
{
	
	public interface IScriptFolderExecutor
	{
		void ExecuteScriptsInFolder(string scriptBaseDirectory, string scriptDirectory, ConnectionSettings settings, ITaskObserver taskObserver);
	}
}