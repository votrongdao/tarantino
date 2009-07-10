using NUnit.Framework;
using Rhino.Mocks;
using Tarantino.Core.DatabaseManager.Model;
using Tarantino.Core.DatabaseManager.Services;
using Tarantino.Core.DatabaseManager.Services.Impl;

namespace Tarantino.UnitTests.Core.DatabaseManager.Services
{
	[TestFixture]
	public class ScriptFolderExecutorTester
	{
		[Test]
		public void Executes_all_the_scripts_within_a_folder()
		{
			var settings = new ConnectionSettings("server", "db", true, null, null);
            var sqlFiles = new[] { "c:\\scripts\\Update\\001.sql", "c:\\scripts\\Update\\002_data_.sql", "c:\\scripts\\Update\\003.sql" };
            var taskAttributes = new TaskAttributes(settings, "c:\\scripts")
                                     {
                                         RequestedDatabaseAction = RequestedDatabaseAction.Update,
                                     };

			var mocks = new MockRepository();
			var initializer = mocks.CreateMock<ISchemaInitializer>();
			var fileLocator = mocks.CreateMock<ISqlFileLocator>();
			var executor = mocks.CreateMock<IChangeScriptExecutor>();
			var versioner = mocks.CreateMock<IDatabaseVersioner>();
			var taskObserver = mocks.CreateMock<ITaskObserver>();
            var fileFilterService = mocks.CreateMock<IFileFilterService>();

			using (mocks.Record())
			{
				initializer.EnsureSchemaCreated(settings);
				Expect.Call(fileLocator.GetSqlFilenames("c:\\scripts", "Update")).Return(sqlFiles);
                Expect.Call(fileFilterService.GetFilteredFilenames(sqlFiles, null)).Return(sqlFiles);
				executor.Execute("c:\\scripts\\Update\\001.sql", settings, taskObserver);
				executor.Execute("c:\\scripts\\Update\\002_data_.sql", settings, taskObserver);
				executor.Execute("c:\\scripts\\Update\\003.sql", settings, taskObserver);
				versioner.VersionDatabase(settings, taskObserver);
			}

			using (mocks.Playback())
			{
                IScriptFolderExecutor folderExecutor = new ScriptFolderExecutor(initializer, fileLocator, executor, versioner, fileFilterService);
				folderExecutor.ExecuteScriptsInFolder(taskAttributes, "Update", taskObserver);
			}

			mocks.VerifyAll();
		}

        [Test]
        public void Executes_only_filtereded_scripts_within_a_folder()
        {
            var settings = new ConnectionSettings("server", "db", true, null, null);
            var sqlFiles = new[] { "c:\\scripts\\Update\\001.sql", "c:\\scripts\\Update\\002_data_.sql", "c:\\scripts\\Update\\003.sql" };
            var filteredFiles = new[] { "c:\\scripts\\Update\\001.sql", "c:\\scripts\\Update\\003.sql" };
            var taskAttributes = new TaskAttributes(settings, "c:\\scripts")
            {
                RequestedDatabaseAction = RequestedDatabaseAction.Update,
                SkipFileNameContaining = "_data_"
            };

            var mocks = new MockRepository();
            var initializer = mocks.CreateMock<ISchemaInitializer>();
            var fileLocator = mocks.CreateMock<ISqlFileLocator>();
            var executor = mocks.CreateMock<IChangeScriptExecutor>();
            var versioner = mocks.CreateMock<IDatabaseVersioner>();
            var taskObserver = mocks.CreateMock<ITaskObserver>();
            var fileFilterService = mocks.CreateMock<IFileFilterService>();

            using (mocks.Record())
            {
                initializer.EnsureSchemaCreated(settings);
                Expect.Call(fileLocator.GetSqlFilenames("c:\\scripts", "Update")).Return(sqlFiles);
                Expect.Call(fileFilterService.GetFilteredFilenames(sqlFiles, "_data_")).Return(filteredFiles);
                executor.Execute("c:\\scripts\\Update\\001.sql", settings, taskObserver);
                executor.Execute("c:\\scripts\\Update\\003.sql", settings, taskObserver);
                versioner.VersionDatabase(settings, taskObserver);
            }

            using (mocks.Playback())
            {
                IScriptFolderExecutor folderExecutor = new ScriptFolderExecutor(initializer, fileLocator, executor, versioner, fileFilterService);
                folderExecutor.ExecuteScriptsInFolder(taskAttributes, "Update", taskObserver);
            }

            mocks.VerifyAll();
        }
	}
}