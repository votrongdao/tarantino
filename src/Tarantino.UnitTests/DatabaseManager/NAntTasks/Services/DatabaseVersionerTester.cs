using System;
using Tarantino.Commons.Core.Services.Environment;
using Tarantino.DatabaseManager.NAntTasks.Domain;
using Tarantino.DatabaseManager.NAntTasks.Services;
using Tarantino.DatabaseManager.NAntTasks.Services.Impl;
using NUnit.Framework;
using Rhino.Mocks;

namespace Tarantino.UnitTests.DatabaseManager.NAntTasks.Services
{
	[TestFixture]
	public class DatabaseVersionerTester
	{
		[Test]
		public void CorrectlyVersionsDatabase()
		{
			ConnectionSettings settings = 
				new ConnectionSettings(String.Empty, String.Empty, false, String.Empty, String.Empty);
			string sqlScript = "SQL script...";

			MockRepository mocks = new MockRepository();
			IResourceFileLocator fileLocator = mocks.CreateMock<IResourceFileLocator>();
			IQueryExecutor queryExecutor = mocks.CreateMock<IQueryExecutor>();
			ITaskObserver taskObserver = mocks.CreateMock<ITaskObserver>();

			Expect.Call(fileLocator.ReadTextFile("Tarantino.DatabaseManager.NAntTasks", "Tarantino.DatabaseManager.NAntTasks.Files.VersionDatabase.sql")).Return(sqlScript);
			Expect.Call(queryExecutor.ExecuteScalarInteger(settings, sqlScript)).Return(7);
			taskObserver.SetVariable("usdDatabaseVersion", "7");

			mocks.ReplayAll();

			IDatabaseVersioner versioner = new DatabaseVersioner(fileLocator, queryExecutor);
            versioner.VersionDatabase(settings, taskObserver, "usdDatabaseVersion");

			mocks.VerifyAll();
		}
	}
}