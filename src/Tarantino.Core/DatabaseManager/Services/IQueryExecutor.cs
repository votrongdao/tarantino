using Tarantino.DatabaseManager.NAntTasks.Domain;

namespace Tarantino.DatabaseManager.NAntTasks.Services
{
	public interface IQueryExecutor
	{
		void ExecuteNonQuery(ConnectionSettings settings, string sql);
		int ExecuteScalarInteger(ConnectionSettings settings, string sql);
		string[] ReadFirstColumnAsStringArray(ConnectionSettings settings, string sql);
	}
}