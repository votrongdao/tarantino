using StructureMap;
using Tarantino.Core;
using Tarantino.DatabaseManager.Model;

namespace Tarantino.Core.DatabaseManager.Services
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IQueryExecutor
	{
		void ExecuteNonQuery(ConnectionSettings settings, string sql, bool runAgainstSpecificDatabase);
		int ExecuteScalarInteger(ConnectionSettings settings, string sql);
		string[] ReadFirstColumnAsStringArray(ConnectionSettings settings, string sql);
	}
}