using Tarantino.Commons.Core.Services.Environment;
using Tarantino.DatabaseManager.NAntTasks.Domain;

namespace Tarantino.DatabaseManager.NAntTasks.Services.Impl
{
	public class DatabaseVersioner : IDatabaseVersioner
	{
		private IQueryExecutor _executor;
		private IResourceFileLocator _fileLocator;

		public DatabaseVersioner(IResourceFileLocator fileLocator, IQueryExecutor executor)
		{
			_fileLocator = fileLocator;
			_executor = executor;
		}

		public void VersionDatabase(ConnectionSettings settings, ITaskObserver taskObserver, string databaseVersionPropertyName)
		{
			string sqlFile = "Tarantino.DatabaseManager.NAntTasks.Files.VersionDatabase.sql";
			string sql = _fileLocator.ReadTextFile("Tarantino.DatabaseManager.NAntTasks", sqlFile);
			string version = _executor.ExecuteScalarInteger(settings, sql).ToString();
			taskObserver.SetVariable(databaseVersionPropertyName, version);
		}
	}
}