using StructureMap;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.DatabaseManager.Model;

namespace Tarantino.Core.DatabaseManager.Services.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class SchemaInitializer : ISchemaInitializer
	{
		private readonly IQueryExecutor _executor;
		private readonly IResourceFileLocator _locator;

		public SchemaInitializer(IResourceFileLocator locator, IQueryExecutor executor)
		{
			_locator = locator;
			_executor = executor;
		}

		public void EnsureSchemaCreated(ConnectionSettings settings)
		{
			string assembly = DatabaseUpgrader.SQL_FILE_ASSEMBLY;
			string sqlFile = string.Format(DatabaseUpgrader.SQL_FILE_TEMPLATE, "CreateSchema");

			string sql = _locator.ReadTextFile(assembly, sqlFile);

			_executor.ExecuteNonQuery(settings, sql);
		}
	}
}