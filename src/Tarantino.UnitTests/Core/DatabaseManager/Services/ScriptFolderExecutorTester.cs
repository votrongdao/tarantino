using NUnit.Framework;
using Rhino.Mocks;
using Tarantino.Core.DatabaseManager.Services;
using Tarantino.Core.DatabaseManager.Services.Impl;
using Tarantino.DatabaseManager.Model;

namespace Tarantino.UnitTests.Core.DatabaseManager.Services
{
	[TestFixture]
	public class ScriptFolderExecutorTester
	{
		[Test]
		public void Executes_the_scripts_within_a_folder()
		{
			ConnectionSettings settings = new ConnectionSettings("server", "db", true, null, null);
			string[] sqlFiles = new string[] { "c:\\scripts\\Update\\001.sql", "c:\\scripts\\Update\\002.sql" };

			MockRepository mocks = new MockRepository();
			ISchemaInitializer initializer = mocks.CreateMock<ISchemaInitializer>();
			ISqlFileLocator fileLocator = mocks.CreateMock<ISqlFileLocator>();
			IChangeScriptExecutor executor = mocks.CreateMock<IChangeScriptExecutor>();
			IDatabaseVersioner versioner = mocks.CreateMock<IDatabaseVersioner>();
			ITaskObserver taskObserver = mocks.CreateMock<ITaskObserver>();

			using (mocks.Record())
			{
				initializer.EnsureSchemaCreated(settings);
				Expect.Call(fileLocator.GetSqlFilenames("c:\\scripts", "Update")).Return(sqlFiles);
				executor.Execute("c:\\scripts\\Update\\001.sql", settings, taskObserver);
				executor.Execute("c:\\scripts\\Update\\002.sql", settings, taskObserver);
				versioner.VersionDatabase(settings, taskObserver);
			}

			using (mocks.Playback())
			{
				IScriptFolderExecutor folderExecutor = new ScriptFolderExecutor(initializer, fileLocator, executor, versioner);
				folderExecutor.ExecuteScriptsInFolder("c:\\scripts", "Update", settings, taskObserver);
			}

			mocks.VerifyAll();
		}
	}
}