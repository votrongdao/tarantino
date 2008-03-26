using NUnit.Framework;
using Rhino.Mocks;
using Tarantino.Core.DatabaseManager.Services;
using Tarantino.Core.DatabaseManager.Services.Impl;
using Tarantino.DatabaseManager.Model;

namespace Tarantino.UnitTests.Core.DatabaseManager.Services
{
	[TestFixture]
	public class DatabaseDropperTester
	{
		[Test]
		public void Drops_database()
		{
			ConnectionSettings settings = new ConnectionSettings("server", "db", true, null, null);

			MockRepository mocks = new MockRepository();
			IDatabaseConnectionDropper connectionDropper = mocks.CreateMock<IDatabaseConnectionDropper>();
			ITaskObserver taskObserver = mocks.CreateMock<ITaskObserver>();
			IQueryExecutor queryExecutor = mocks.CreateMock<IQueryExecutor>();

			using (mocks.Record())
			{
				connectionDropper.Drop(settings, taskObserver);
				queryExecutor.ExecuteNonQuery(settings, "drop database db", false);
			}

			using (mocks.Playback())
			{
				IDatabaseActionExecutor dropper = new DatabaseDropper(connectionDropper, queryExecutor);
				dropper.Execute(null, settings, taskObserver);
			}

			mocks.VerifyAll();
		}
	}
}