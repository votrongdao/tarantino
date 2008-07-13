using Tarantino.Core.DatabaseManager.Model;

namespace Tarantino.Core.DatabaseManager.Services.Impl
{
	public class DatabaseUpdater : IDatabaseActionExecutor
	{
		private readonly IScriptFolderExecutor _folderExecutor;

		public DatabaseUpdater(IScriptFolderExecutor folderExecutor)
		{
			_folderExecutor = folderExecutor;
		}

		public void Execute(string scriptFolder, ConnectionSettings settings, ITaskObserver taskObserver)
		{
			_folderExecutor.ExecuteScriptsInFolder(scriptFolder, "Update", settings, taskObserver);
		}
	}
}