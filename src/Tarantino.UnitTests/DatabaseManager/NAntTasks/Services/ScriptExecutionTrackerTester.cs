using Tarantino.DatabaseManager.NAntTasks.Domain;
using Tarantino.DatabaseManager.NAntTasks.Services;
using Tarantino.DatabaseManager.NAntTasks.Services.Impl;
using NUnit.Framework;
using Rhino.Mocks;

namespace Tarantino.UnitTests.DatabaseManager.NAntTasks.Services
{
	[TestFixture]
	public class ScriptExecutionTrackerTester
	{
		[Test]
		public void CorrectlyDeterminesWhenScriptHasBeenExecuted()
		{
			ConnectionSettings settings = new ConnectionSettings(string.Empty, string.Empty, false, string.Empty, string.Empty);
			string scriptFilename = "02_Test.sql";
			string[] executedScriptFiles = new string[] { "01_Test.sql", "02_Test.sql" };

			MockRepository mocks = new MockRepository();
			IQueryExecutor queryExecutor = mocks.CreateMock<IQueryExecutor>();
			Expect.Call(queryExecutor.ReadFirstColumnAsStringArray(settings, "select ScriptFile from usd_AppliedDatabaseScript")).Return(executedScriptFiles);

			mocks.ReplayAll();

			IScriptExecutionTracker tracker = new ScriptExecutionTracker(queryExecutor);
			bool alreadyExecuted = tracker.ScriptAlreadyExecuted(settings, scriptFilename);
			
			Assert.AreEqual(true, alreadyExecuted);

			mocks.VerifyAll();
		}

		[Test]
		public void CorrectlyDeterminesWhenScriptHasNotBeenExecuted()
		{
			ConnectionSettings settings = new ConnectionSettings(string.Empty, string.Empty, false, string.Empty, string.Empty);
			string scriptFilename = "03_Test.sql";
			string[] executedScriptFiles = new string[] { "01_Test.sql", "02_Test.sql" };

			MockRepository mocks = new MockRepository();
			IQueryExecutor queryExecutor = mocks.CreateMock<IQueryExecutor>();
			Expect.Call(queryExecutor.ReadFirstColumnAsStringArray(settings, "select ScriptFile from usd_AppliedDatabaseScript")).Return(executedScriptFiles);

			mocks.ReplayAll();

			IScriptExecutionTracker tracker = new ScriptExecutionTracker(queryExecutor);
			bool alreadyExecuted = tracker.ScriptAlreadyExecuted(settings, scriptFilename);

			Assert.AreEqual(false, alreadyExecuted);

			mocks.VerifyAll();
		}

		[Test]
		public void CorrectlyMarksScriptAsExecuted()
		{
			ConnectionSettings settings = new ConnectionSettings(string.Empty, string.Empty, false, string.Empty, string.Empty);
			string scriptFilename = "03_Test.sql";

			MockRepository mocks = new MockRepository();
			IQueryExecutor queryExecutor = mocks.CreateMock<IQueryExecutor>();
			ITaskObserver observer = mocks.CreateMock<ITaskObserver>();
			queryExecutor.ExecuteNonQuery(settings, "insert into usd_AppliedDatabaseScript (ScriptFile, DateApplied) values ('03_Test.sql', getdate())");

			mocks.ReplayAll();

			IScriptExecutionTracker tracker = new ScriptExecutionTracker(queryExecutor);
			tracker.MarkScriptAsExecuted(settings, scriptFilename, observer);

			mocks.VerifyAll();
		}
	}
}