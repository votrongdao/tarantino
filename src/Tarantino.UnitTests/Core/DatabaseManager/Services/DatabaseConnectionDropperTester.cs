using Tarantino.Commons.Services.DataFileManagement;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.DatabaseManager.Model;
using Tarantino.DatabaseManager.Services;
using Tarantino.DatabaseManager.Services.Impl;
using NUnit.Framework;
using Rhino.Mocks;

namespace Tarantino.UnitTests.Core.DatabaseManager.Services
{
	[TestFixture]
	public class DatabaseConnectionDropperTester
	{
		[Test]
		public void CorrectlyCoordinatesDatabaseDropTest()
		{
			string assembly = DatabaseUpgrader.SQL_FILE_ASSEMBLY;
			string sqlFile = string.Format(DatabaseUpgrader.SQL_FILE_TEMPLATE, "DropConnections");

			ConnectionSettings settings = new ConnectionSettings("server", "db", true, null, null);

			MockRepository mocks = new MockRepository();
			IResourceFileLocator fileLocator = mocks.CreateMock<IResourceFileLocator>();
			ITokenReplacer replacer = mocks.CreateMock<ITokenReplacer>();
			IQueryExecutor queryExecutor = mocks.CreateMock<IQueryExecutor>();

			Expect.Call(fileLocator.ReadTextFile(assembly, sqlFile)).Return("Unformatted SQL");
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