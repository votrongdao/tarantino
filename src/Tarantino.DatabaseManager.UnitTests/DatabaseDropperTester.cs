using System;
using NUnit.Framework;
using Rhino.Mocks;
using Tarantino.Core.DatabaseManager.Model;
using Tarantino.Core.DatabaseManager.Services;
using Tarantino.Core.DatabaseManager.Services.Impl;

namespace Tarantino.UnitTests.Core.DatabaseManager.Services
{
	[TestFixture]
	public class DatabaseDropperTester
	{
		[Test]
		public void Drops_database()
		{
			var settings = new ConnectionSettings("server", "db", true, null, null);
            var taskAttributes = new TaskAttributes(settings, null);

			var mocks = new MockRepository();
			var connectionDropper = mocks.CreateMock<IDatabaseConnectionDropper>();
			var taskObserver = mocks.CreateMock<ITaskObserver>();
			var queryExecutor = mocks.CreateMock<IQueryExecutor>();
            
			using (mocks.Record())
			{
				connectionDropper.Drop(settings, taskObserver);
                
                queryExecutor.ExecuteNonQuery(settings, "ALTER DATABASE [db] SET SINGLE_USER WITH ROLLBACK IMMEDIATE drop database [db]", false);
			    
			}

			using (mocks.Playback())
			{
				IDatabaseActionExecutor dropper = new DatabaseDropper(connectionDropper, queryExecutor);
				dropper.Execute(taskAttributes, taskObserver);
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Should_not_fail_if_datebase_does_not_exist()
		{
			var settings = new ConnectionSettings("server", "db", true, null, null);
            var taskAttributes = new TaskAttributes(settings, null);

			var mocks = new MockRepository();
			var connectionDropper = mocks.DynamicMock<IDatabaseConnectionDropper>();
			var taskObserver = mocks.CreateMock<ITaskObserver>();
			var queryExecutor = mocks.CreateMock<IQueryExecutor>();

			using (mocks.Record())
			{
                Expect.Call(() => queryExecutor.ExecuteNonQuery(settings, "ALTER DATABASE [db] SET SINGLE_USER WITH ROLLBACK IMMEDIATE drop database [db]", false))
					.Throw(new Exception("foo message"));
				Expect.Call(() => taskObserver.Log("Database 'db' could not be dropped."));
			}

			using (mocks.Playback())
			{
				IDatabaseActionExecutor dropper = new DatabaseDropper(connectionDropper, queryExecutor);
                dropper.Execute(taskAttributes, taskObserver);
			}

			mocks.VerifyAll();
		}
	}
}