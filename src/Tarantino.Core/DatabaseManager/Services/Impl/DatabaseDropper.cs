using System;

using Tarantino.DatabaseManager.Model;

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

		public void Execute(string scriptFolder, ConnectionSettings settings, ITaskObserver taskObserver)
		{
			_connectionDropper.Drop(settings, taskObserver);
			var sql = string.Format("drop database {0}", settings.Database);

			try
			{
				_queryExecutor.ExecuteNonQuery(settings, sql, false);
			}
			catch(Exception)
			{
				taskObserver.Log(string.Format("Database '{0}' could not be dropped.",
					settings.Database));
			}
		}
	}
}