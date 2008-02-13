using Tarantino.Commons.Core.Services.Environment;
using Tarantino.DatabaseManager.NAntTasks.Domain;
using Tarantino.DatabaseManager.NAntTasks.Services;
using Tarantino.DatabaseManager.NAntTasks.Services.Impl;
using NUnit.Framework;
using Rhino.Mocks;

namespace Tarantino.UnitTests.DatabaseManager.NAntTasks.Services
{
	[TestFixture]
	public class DatabaseConnectionDropperTester
	{
		[Test]
		public void CorrectlyCoordinatesDatabaseDropTest()
		{
			ConnectionSettings settings = new ConnectionSettings("server", "db", true, null, null);

			MockRepository mocks = new MockRepository();
			IResourceFileLocator fileLocator = mocks.CreateMock<IResourceFileLocator>();
			ITokenReplacer replacer = mocks.CreateMock<ITokenReplacer>();
			IQueryExecutor queryExecutor = mocks.CreateMock<IQueryExecutor>();

			Expect.Call(fileLocator.ReadTextFile("Tarantino.DatabaseManager.NAntTasks", "Tarantino.DatabaseManager.NAntTasks.Files.DropConnections.sql")).Return("Unformatted SQL");
			replacer.Text = "Unformatted SQL";
			replacer.Replace("DatabaseName", "MyDatabase");
			Expect.Call(replacer.Text).Return("Formatted SQL");
			queryExecutor.ExecuteNonQuery(settings, "Formatted SQL");

			mocks.ReplayAll();

			IDatabaseConnectionDropper dropper = new DatabaseConnectionDropper(fileLocator, replacer, queryExecutor);
			dropper.Drop("MyDatabase", settings);

			mocks.VerifyAll();
		}
	}
}