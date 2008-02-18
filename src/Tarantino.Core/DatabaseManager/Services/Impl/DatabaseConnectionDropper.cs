using StructureMap;
using Tarantino.Core.Commons.Services.DataFileManagement;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.DatabaseManager.Model;

namespace Tarantino.Core.DatabaseManager.Services.Impl
{
	[Pluggable(ServiceKeys.Default)]
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
			string assembly = DatabaseUpgrader.SQL_FILE_ASSEMBLY;
			string sqlFile = string.Format(DatabaseUpgrader.SQL_FILE_TEMPLATE, "DropConnections");

			string sql = _fileLocator.ReadTextFile(assembly, sqlFile);

			_replacer.Text = sql;
			_replacer.Replace("DatabaseName", databaseName);
			sql = _replacer.Text;

			_executor.ExecuteNonQuery(settings, sql);
		}
	}
}