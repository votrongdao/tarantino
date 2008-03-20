using System.Collections.Generic;
using StructureMap;
using Tarantino.Core;
using Tarantino.DatabaseManager.Model;
using Tarantino.Core.DatabaseManager.Services.Impl;

namespace Tarantino.Core.DatabaseManager.Services.Impl
{
	[Pluggable(Keys.Default)]
	public class SqlDatabaseManager : ISqlDatabaseManager
	{
		public const string SQL_FILE_ASSEMBLY = "Tarantino.Core";
		public const string SQL_FILE_TEMPLATE = "Tarantino.Core.DatabaseManager.SqlFiles.{0}.sql";

		private readonly ILogMessageGenerator _logMessageGenerator;
		private readonly IDatabaseActionExecutorFactory _actionExecutorFactory;

		public SqlDatabaseManager(ILogMessageGenerator logMessageGenerator, IDatabaseActionExecutorFactory actionExecutorFactory)
		{
			_logMessageGenerator = logMessageGenerator;
			_actionExecutorFactory = actionExecutorFactory;
		}

		public void Upgrade(string scriptDirectory, string server, string database, bool integrated, string username, string password, RequestedDatabaseAction requestedAction, ITaskObserver taskObserver)
		{
			ConnectionSettings settings = new ConnectionSettings(server, database, integrated, username, password);

			string initializationMessage = _logMessageGenerator.GetInitialMessage(requestedAction, scriptDirectory, settings);
			taskObserver.Log(initializationMessage);

			IEnumerable<IDatabaseActionExecutor> executors = _actionExecutorFactory.GetExecutors(requestedAction);

			foreach (IDatabaseActionExecutor executor in executors)
			{
				executor.Execute(scriptDirectory, settings, taskObserver);
			}
		}
	}
}