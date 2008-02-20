using NUnit.Framework;
using Rhino.Mocks;
using Tarantino.Core.DatabaseManager.Services;
using Tarantino.Core.DatabaseManager.Services.Impl;
using Tarantino.DatabaseManager.Model;

namespace Tarantino.UnitTests.Core.DatabaseManager.Services
{
	[TestFixture]
	public class DatabaseCreatorTester
	{
		[Test]
		public void Creates_database()
		{
			ConnectionSettings settings = new ConnectionSettings("server", "db", true, null, null);

			MockRepository mocks = new MockRepository();
			IQueryExecutor queryExecutor = mocks.CreateMock<IQueryExecutor>();
			IScriptFolderExecutor executor = mocks.CreateMock<IScriptFolderExecutor>();
			ITaskObserver taskObserver = mocks.CreateMock<ITaskObserver>();
			
			using (mocks.Record())
			{
				queryExecutor.ExecuteNonQuery(settings, "create database db", false);
				executor.ExecuteScriptsInFolder("c:\\scripts", "ExistingSchema", settings, taskObserver);
			}

			using (mocks.Playback())
			{
				IDatabaseActionExecutor creator = new DatabaseCreator(queryExecutor, executor);
				creator.Execute("c:\\scripts", settings, taskObserver);
			}

			mocks.VerifyAll();
		}
	}
}