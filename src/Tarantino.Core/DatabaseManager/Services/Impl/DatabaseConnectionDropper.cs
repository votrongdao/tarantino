using Tarantino.Core.Commons.Services.Environment;
using Tarantino.DatabaseManager.Model;

namespace Tarantino.DatabaseManager.Services.Impl
{
	public class DatabaseConnectionDropper : IDatabaseConnectionDropper
	{
		private IResourceFileLocator _fileLocator;
		private ITokenReplacer _replacer;
		private IQueryExecutor _executor;

		public DatabaseConnectionDropper(IResourceFileLocator fileLocator, ITokenReplacer replacer, IQueryExecutor executor)
		{
			_fileLocator = fileLocator;
			_replacer = replacer;
			_executor = executor;
		}

		public void Drop(string databaseName, ConnectionSettings settings)
		{
			string sql = _fileLocator.ReadTextFile("Tarantino.DatabaseManager.NAntTasks", "Tarantino.DatabaseManager.NAntTasks.Files.DropConnections.sql");

			_replacer.Text = sql;
			_replacer.Replace("DatabaseName", databaseName);
			sql = _replacer.Text;

			_executor.ExecuteNonQuery(settings, sql);
		}
	}
}