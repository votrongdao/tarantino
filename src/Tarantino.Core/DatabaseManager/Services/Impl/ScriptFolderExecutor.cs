using StructureMap;
using Tarantino.DatabaseManager.Model;

namespace Tarantino.Core.DatabaseManager.Services.Impl
{
	[Pluggable(Keys.Default)]
	public class ScriptFolderExecutor : IScriptFolderExecutor
	{
		private readonly ISchemaInitializer _schemaInitializer;
		private readonly ISqlFileLocator _fileLocator;
		private readonly IChangeScriptExecutor _scriptExecutor;
		private readonly IDatabaseVersioner _versioner;

		public ScriptFolderExecutor(ISchemaInitializer schemaInitializer, ISqlFileLocator fileLocator, IChangeScriptExecutor scriptExecutor, IDatabaseVersioner versioner)
		{
			_schemaInitializer = schemaInitializer;
			_fileLocator = fileLocator;
			_scriptExecutor = scriptExecutor;
			_versioner = versioner;
		}

		public void ExecuteScriptsInFolder(string scriptBaseDirectory, string scriptDirectory, ConnectionSettings settings, ITaskObserver taskObserver)
		{
			_schemaInitializer.EnsureSchemaCreated(settings);
			string[] sqlFilenames = _fileLocator.GetSqlFilenames(scriptBaseDirectory, scriptDirectory);

			foreach (string sqlFilename in sqlFilenames)
			{
				_scriptExecutor.Execute(sqlFilename, settings, taskObserver);
			}

			_versioner.VersionDatabase(settings, taskObserver);
		}
	}
}