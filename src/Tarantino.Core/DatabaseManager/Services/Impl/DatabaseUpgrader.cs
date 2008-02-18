using StructureMap;
using Tarantino.Core;
using Tarantino.DatabaseManager.Model;
using Tarantino.DatabaseManager.Services.Impl;

namespace Tarantino.DatabaseManager.Services.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class DatabaseUpgrader : IDatabaseUpgrader
	{
		public const string SQL_FILE_ASSEMBLY = "Tarantino.Core";
		public const string SQL_FILE_TEMPLATE = "Tarantino.Core.DatabaseManager.SqlFiles.{0}.sql";

		private IQueryExecutor _executor;
		private ISqlFileLocator _fileLocator;
		private ISchemaInitializer _schemaInitializer;
		private IChangeScriptExecutor _scriptExecutor;
		private readonly IDatabaseVersioner _versioner;
		private readonly IDatabaseConnectionDropper _connectionDropper;

		public DatabaseUpgrader(IQueryExecutor executor, ISchemaInitializer schemaInitializer, ISqlFileLocator fileLocator, IChangeScriptExecutor scriptExecutor, IDatabaseVersioner versioner, IDatabaseConnectionDropper connectionDropper)
		{
			_executor = executor;
			_schemaInitializer = schemaInitializer;
			_fileLocator = fileLocator;
			_scriptExecutor = scriptExecutor;
			_versioner = versioner;
			_connectionDropper = connectionDropper;
		}

		public void Upgrade(string scriptDirectory, string server, string database, bool integrated, string username, string password, DatabaseAction action, ITaskObserver taskObserver, string databaseVersionPropertyName)
		{
			string scriptsClause = action != DatabaseAction.Drop
			                       	? string.Format(" using scripts from {0}", scriptDirectory)
			                       	: string.Empty;

			string logMessage =
				string.Format("{0} {1} on {2}{3}\n", action, database, server, scriptsClause);

			taskObserver.Log(logMessage);

			ConnectionSettings settings = new ConnectionSettings(server, null, integrated, username, password);

			if (action == DatabaseAction.Drop || action == DatabaseAction.Rebuild)
			{
				taskObserver.Log(string.Format("Dropping connections for database {0}\n", database));
				_connectionDropper.Drop(database, settings);
				_executor.ExecuteNonQuery(settings, string.Format("drop database {0}", database));
			}

			if (action != DatabaseAction.Drop)
			{
				if (action == DatabaseAction.Create || action == DatabaseAction.Rebuild)
				{
					_executor.ExecuteNonQuery(settings, string.Format("create database {0}", database));
				}

				ConnectionSettings settingsWithDatabase = new ConnectionSettings(server, database, integrated, username, password);
				_schemaInitializer.EnsureSchemaCreated(settingsWithDatabase);
				string[] sqlFilenames = _fileLocator.GetSqlFilenames(scriptDirectory, action);

				foreach (string str in sqlFilenames)
				{
					_scriptExecutor.Execute(str, settingsWithDatabase, taskObserver);
				}
				_versioner.VersionDatabase(settingsWithDatabase, taskObserver, databaseVersionPropertyName);
			}
		}
	}
}