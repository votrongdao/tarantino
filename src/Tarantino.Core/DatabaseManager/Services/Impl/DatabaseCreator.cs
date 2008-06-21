using Tarantino.DatabaseManager.Model;

namespace Tarantino.Core.DatabaseManager.Services.Impl
{
	public class DatabaseCreator : IDatabaseActionExecutor
	{
		private readonly IQueryExecutor _queryExecutor;
		private readonly IScriptFolderExecutor _folderExecutor;

		public DatabaseCreator(IQueryExecutor queryExecutor, IScriptFolderExecutor folderExecutor)
		{
			_queryExecutor = queryExecutor;
			_folderExecutor = folderExecutor;
		}

		public void Execute(string scriptFolder, ConnectionSettings settings, ITaskObserver taskObserver)
		{
			string sql = string.Format("create database {0}", settings.Database);
			_queryExecutor.ExecuteNonQuery(settings, sql, false);

			_folderExecutor.ExecuteScriptsInFolder(scriptFolder, "ExistingSchema", settings, taskObserver);
		}
	}
}