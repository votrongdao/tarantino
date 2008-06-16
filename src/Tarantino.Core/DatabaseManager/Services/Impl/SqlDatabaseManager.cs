using System.Collections.Generic;
using Tarantino.DatabaseManager.Model;

namespace Tarantino.Core.DatabaseManager.Services.Impl
{
	public class SqlDatabaseManager : ISqlDatabaseManager
	{
		public const string SQL_FILE_ASSEMBLY = "Tarantino.Core";
		public const string SQL_FILE_TEMPLATE = "Tarantino.Core.DatabaseManager.SqlFiles.{0}.sql";

		private readonly IDatabaseActionExecutorFactory _actionExecutorFactory;
		private readonly ILogMessageGenerator _logMessageGenerator;

		public SqlDatabaseManager(ILogMessageGenerator logMessageGenerator,
		                          IDatabaseActionExecutorFactory actionExecutorFactory)
		{
			_logMessageGenerator = logMessageGenerator;
			_actionExecutorFactory = actionExecutorFactory;
		}

		#region ISqlDatabaseManager Members

		public void Upgrade(string scriptDirectory, string server, string database, bool integrated, string username,
		                    string password, RequestedDatabaseAction requestedAction, ITaskObserver taskObserver)
		{
			var settings = new ConnectionSettings(server, database, integrated, username, password);

			string initializationMessage = _logMessageGenerator.GetInitialMessage(requestedAction, scriptDirectory, settings);
			taskObserver.Log(initializationMessage);

			IEnumerable<IDatabaseActionExecutor> executors = _actionExecutorFactory.GetExecutors(requestedAction);

			foreach (IDatabaseActionExecutor executor in executors)
			{
				executor.Execute(scriptDirectory, settings, taskObserver);
			}
		}

		#endregion
	}
}