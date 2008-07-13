using NUnit.Framework;
using Rhino.Mocks;
using Tarantino.Core.DatabaseManager.Model;
using Tarantino.Core.DatabaseManager.Services;
using Tarantino.Core.DatabaseManager.Services.Impl;

namespace Tarantino.UnitTests.Core.DatabaseManager.Services
{
	[TestFixture]
	public class SqlDatabaseManagerTester
	{
		[Test]
		public void Manages_database()
		{
			ConnectionSettings settings = new ConnectionSettings("server", "db", true, null, null);
			string scriptDirectory = @"c:\scripts";

			MockRepository mocks = new MockRepository();
			ITaskObserver taskObserver = mocks.CreateMock<ITaskObserver>();
			ILogMessageGenerator generator = mocks.CreateMock<ILogMessageGenerator>();
			IDatabaseActionExecutorFactory factory = mocks.CreateMock<IDatabaseActionExecutorFactory>();

			IDatabaseActionExecutor creator = mocks.CreateMock<IDatabaseActionExecutor>();
			IDatabaseActionExecutor updater = mocks.CreateMock<IDatabaseActionExecutor>();

			IDatabaseActionExecutor[] executors = new IDatabaseActionExecutor[] { creator, updater };

			using (mocks.Record())
			{
				Expect.Call(generator.GetInitialMessage(RequestedDatabaseAction.Create, scriptDirectory, settings)).Return("starting...");
				taskObserver.Log("starting...");
				Expect.Call(factory.GetExecutors(RequestedDatabaseAction.Create)).Return(executors);

				creator.Execute("c:\\scripts", settings, taskObserver);
				updater.Execute("c:\\scripts", settings, taskObserver);
			}

			using (mocks.Playback())
			{
				ISqlDatabaseManager manager = new SqlDatabaseManager(generator, factory);

				manager.Upgrade(scriptDirectory, settings.Server, settings.Database, settings.IntegratedAuthentication, null, null, RequestedDatabaseAction.Create, taskObserver);
			}

			mocks.VerifyAll();
		}
	}
}