using NUnit.Framework;
using Rhino.Mocks;
using Tarantino.DatabaseManager.Model;
using Tarantino.DatabaseManager.Services;
using Tarantino.DatabaseManager.Services.Impl;

namespace Tarantino.UnitTests.Core.DatabaseManager.Services
{
	[TestFixture]
	public class DatabaseUpgraderTester
	{
		[Test]
		public void CorrectlyCreatesNewDatabase()
		{
			ConnectionSettings settings = new ConnectionSettings("server", "db", true, null, null);
			ConnectionSettings settingsWithoutDatabase = new ConnectionSettings("server", null, true, null, null);

			string scriptDirectory = @"c:\scripts";
			string createScript = @"c:\scripts\Create\01_Create.sql";
			string updateScript = @"c:\scripts\Update\01_Update.sql";
			string[] sqlFiles = new string[] {createScript, updateScript};

			MockRepository mocks = new MockRepository();
			IQueryExecutor queryExecutor = mocks.CreateMock<IQueryExecutor>();
			ISchemaInitializer initializer = mocks.CreateMock<ISchemaInitializer>();
			ISqlFileLocator fileLocator = mocks.CreateMock<ISqlFileLocator>();
			IChangeScriptExecutor scriptExecutor = mocks.CreateMock<IChangeScriptExecutor>();
			IDatabaseVersioner versioner = mocks.CreateMock<IDatabaseVersioner>();
			ITaskObserver taskObserver = mocks.CreateMock<ITaskObserver>();

			taskObserver.Log("Create db on server using scripts from c:\\scripts\n");
			queryExecutor.ExecuteNonQuery(settingsWithoutDatabase, "create database db");
			initializer.EnsureSchemaCreated(settings);
			Expect.Call(fileLocator.GetSqlFilenames(scriptDirectory, DatabaseAction.Create)).Return(sqlFiles);
			scriptExecutor.Execute(createScript, settings, taskObserver);
			scriptExecutor.Execute(updateScript, settings, taskObserver);
			versioner.VersionDatabase(settings, taskObserver, "usdDatabaseVersion");

			mocks.ReplayAll();

			IDatabaseUpgrader upgrader =
				new DatabaseUpgrader(queryExecutor, initializer, fileLocator, scriptExecutor, versioner, null);
			upgrader.Upgrade(scriptDirectory, settings.Server, settings.Database,
			                 settings.IntegratedAuthentication, null, null, DatabaseAction.Create, taskObserver,
			                 "usdDatabaseVersion");

			mocks.VerifyAll();
		}

		[Test]
		public void CorrectlyRebuildsDatabase()
		{
			ConnectionSettings settings = new ConnectionSettings("server", "db", true, null, null);
			ConnectionSettings settingsWithoutDatabase = new ConnectionSettings("server", null, true, null, null);

			string scriptDirectory = @"c:\scripts";
			string createScript = @"c:\scripts\Create\01_Create.sql";
			string updateScript = @"c:\scripts\Update\01_Update.sql";
			string[] sqlFiles = new string[] {createScript, updateScript};

			MockRepository mocks = new MockRepository();
			IDatabaseConnectionDropper connectionDropper = mocks.CreateMock<IDatabaseConnectionDropper>();
			IQueryExecutor queryExecutor = mocks.CreateMock<IQueryExecutor>();
			ISchemaInitializer initializer = mocks.CreateMock<ISchemaInitializer>();
			ISqlFileLocator fileLocator = mocks.CreateMock<ISqlFileLocator>();
			IChangeScriptExecutor scriptExecutor = mocks.CreateMock<IChangeScriptExecutor>();
			IDatabaseVersioner versioner = mocks.CreateMock<IDatabaseVersioner>();
			ITaskObserver taskObserver = mocks.CreateMock<ITaskObserver>();

			taskObserver.Log("Rebuild db on server using scripts from c:\\scripts\n");
			taskObserver.Log("Dropping connections for database db\n");
			connectionDropper.Drop("db", settingsWithoutDatabase);
			queryExecutor.ExecuteNonQuery(settingsWithoutDatabase, "drop database db");
			queryExecutor.ExecuteNonQuery(settingsWithoutDatabase, "create database db");
			initializer.EnsureSchemaCreated(settings);
			Expect.Call(fileLocator.GetSqlFilenames(scriptDirectory, DatabaseAction.Rebuild)).Return(sqlFiles);
			scriptExecutor.Execute(createScript, settings, taskObserver);
			scriptExecutor.Execute(updateScript, settings, taskObserver);
			versioner.VersionDatabase(settings, taskObserver, "usdDatabaseVersion");

			mocks.ReplayAll();

			IDatabaseUpgrader upgrader = new DatabaseUpgrader(queryExecutor, initializer, fileLocator, scriptExecutor, versioner, connectionDropper);

			upgrader.Upgrade(scriptDirectory, settings.Server, settings.Database, 
				settings.IntegratedAuthentication, null, null, DatabaseAction.Rebuild, taskObserver,
				"usdDatabaseVersion");

			mocks.VerifyAll();
		}

		[Test]
		public void CorrectlyUpdatesExistingDatabase()
		{
			ConnectionSettings settings = new ConnectionSettings("server", "db", true, null, null);

			string scriptDirectory = @"c:\scripts";
			string createScript = @"c:\scripts\Create\01_Create.sql";
			string updateScript = @"c:\scripts\Update\01_Update.sql";
			string[] sqlFiles = new string[] {createScript, updateScript};

			MockRepository mocks = new MockRepository();
			IQueryExecutor queryExecutor = mocks.CreateMock<IQueryExecutor>();
			ISchemaInitializer initializer = mocks.CreateMock<ISchemaInitializer>();
			ISqlFileLocator fileLocator = mocks.CreateMock<ISqlFileLocator>();
			IChangeScriptExecutor scriptExecutor = mocks.CreateMock<IChangeScriptExecutor>();
			IDatabaseVersioner versioner = mocks.CreateMock<IDatabaseVersioner>();
			ITaskObserver taskObserver = mocks.CreateMock<ITaskObserver>();

			taskObserver.Log("Update db on server using scripts from c:\\scripts\n");
			initializer.EnsureSchemaCreated(settings);
			Expect.Call(fileLocator.GetSqlFilenames(scriptDirectory, DatabaseAction.Update)).Return(sqlFiles);
			scriptExecutor.Execute(createScript, settings, taskObserver);
			scriptExecutor.Execute(updateScript, settings, taskObserver);
			versioner.VersionDatabase(settings, taskObserver, "usdDatabaseVersion");

			mocks.ReplayAll();

			IDatabaseUpgrader upgrader =
				new DatabaseUpgrader(queryExecutor, initializer, fileLocator, scriptExecutor, versioner, null);
			upgrader.Upgrade(scriptDirectory, settings.Server, settings.Database,
			                 settings.IntegratedAuthentication, null, null, DatabaseAction.Update, taskObserver,
			                 "usdDatabaseVersion");

			mocks.VerifyAll();
		}

		[Test]
		public void CorrectlyDropsDatabase()
		{
			ConnectionSettings settings = new ConnectionSettings("server", "db", true, null, null);
			ConnectionSettings settingsWithoutDatabase = new ConnectionSettings("server", null, true, null, null);

			MockRepository mocks = new MockRepository();
			IDatabaseConnectionDropper connectionDropper = mocks.CreateMock<IDatabaseConnectionDropper>();
			IQueryExecutor queryExecutor = mocks.CreateMock<IQueryExecutor>();
			ITaskObserver taskObserver = mocks.CreateMock<ITaskObserver>();

			taskObserver.Log("Drop db on server\n");
			connectionDropper.Drop("db", settingsWithoutDatabase);
			queryExecutor.ExecuteNonQuery(settingsWithoutDatabase, "drop database db");
			taskObserver.Log("Dropping connections for database db\n");

			mocks.ReplayAll();

			IDatabaseUpgrader upgrader = new DatabaseUpgrader(queryExecutor, null, null, null, null, connectionDropper);

			upgrader.Upgrade(null, settings.Server, settings.Database,
			                 settings.IntegratedAuthentication, null, null, DatabaseAction.Drop, taskObserver, "usdDatabaseVersion");

			mocks.VerifyAll();
		}
	}
}