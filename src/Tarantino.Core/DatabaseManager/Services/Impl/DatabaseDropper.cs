using System;
using Tarantino.Core.DatabaseManager.Model;

namespace Tarantino.Core.DatabaseManager.Services.Impl
{
	public class DatabaseDropper : IDatabaseActionExecutor
	{
		private readonly IDatabaseConnectionDropper _connectionDropper;
		private readonly IQueryExecutor _queryExecutor;

		public DatabaseDropper(IDatabaseConnectionDropper connectionDropper, IQueryExecutor queryExecutor)
		{
			_connectionDropper = connectionDropper;
			_queryExecutor = queryExecutor;
		}

		public void Execute(TaskAttributes taskAttributes, ITaskObserver taskObserver)
		{
	               _connectionDropper.Drop(taskAttributes.ConnectionSettings, taskObserver);
			var sql = string.Format("ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE drop database {0}", taskAttributes.ConnectionSettings.Database);

			try
			{
                _queryExecutor.ExecuteNonQuery(taskAttributes.ConnectionSettings, sql, false);
			}
			catch(Exception)
			{
				taskObserver.Log(string.Format("Database '{0}' could not be dropped.", taskAttributes.ConnectionSettings.Database));
			}
		}
	}
}