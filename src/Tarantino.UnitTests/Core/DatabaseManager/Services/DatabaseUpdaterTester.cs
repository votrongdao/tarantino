using NUnit.Framework;
using Rhino.Mocks;
using Tarantino.Core.DatabaseManager.Services;
using Tarantino.Core.DatabaseManager.Services.Impl;
using Tarantino.DatabaseManager.Model;

namespace Tarantino.UnitTests.Core.DatabaseManager.Services
{
	[TestFixture]
	public class DatabaseUpdaterTester
	{
		[Test]
		public void Updates_database()
		{
			ConnectionSettings settings = new ConnectionSettings("server", "db", true, null, null);

			MockRepository mocks = new MockRepository();
			IScriptFolderExecutor executor = mocks.CreateMock<IScriptFolderExecutor>();
			ITaskObserver taskObserver = mocks.CreateMock<ITaskObserver>();

			using (mocks.Record())
			{
				executor.ExecuteScriptsInFolder("c:\\scripts", "Update", settings, taskObserver);
			}

			using (mocks.Playback())
			{
				IDatabaseActionExecutor updater = new DatabaseUpdater(executor);
				updater.Execute("c:\\scripts", settings, taskObserver);
			}

			mocks.VerifyAll();
		}
	}
}