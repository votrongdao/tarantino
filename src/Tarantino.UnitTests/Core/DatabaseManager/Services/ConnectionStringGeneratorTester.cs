using Tarantino.DatabaseManager.Model;
using Tarantino.DatabaseManager.Services;
using Tarantino.DatabaseManager.Services.Impl;
using NUnit.Framework;

namespace Tarantino.UnitTests.Core.DatabaseManager.Services
{
	[TestFixture]
	public class ConnectionStringGeneratorTester
	{
		[Test]
		public void CorrectlyGeneratesConnectionStringWithDatabaseAndIntegratedSecurity()
		{
			IConnectionStringGenerator generator = new ConnectionStringGenerator();

			ConnectionSettings settings = new ConnectionSettings("server", "db", true, string.Empty, string.Empty);
			string connString = generator.GetConnectionString(settings);
			
			Assert.AreEqual("Data Source=server;Initial Catalog=db;Integrated Security=True;", connString);
		}
		
		[Test]
		public void CorrectlyGeneratesConnectionStringWithoutDatabaseAndIntegratedSecurity()
		{
			IConnectionStringGenerator generator = new ConnectionStringGenerator();

			ConnectionSettings settings = new ConnectionSettings("server", string.Empty, true, string.Empty, string.Empty);
			string connString = generator.GetConnectionString(settings);
			
			Assert.AreEqual("Data Source=server;Integrated Security=True;", connString);
		}
		
		[Test]
		public void CorrectlyGeneratesConnectionStringWithDatabaseAndUserSecurity()
		{
			IConnectionStringGenerator generator = new ConnectionStringGenerator();

			ConnectionSettings settings = new ConnectionSettings("server", "db", false, "usr", "pwd");
			string connString = generator.GetConnectionString(settings);
			
			Assert.AreEqual("Data Source=server;Initial Catalog=db;User ID=usr;Password=pwd;", connString);
		}
		
		[Test]
		public void CorrectlyGeneratesConnectionStringWithoutDatabaseAndUserSecurity()
		{
			IConnectionStringGenerator generator = new ConnectionStringGenerator();

			ConnectionSettings settings = new ConnectionSettings("server", string.Empty, false, "usr", "pwd");
			string connString = generator.GetConnectionString(settings);

			Assert.AreEqual("Data Source=server;User ID=usr;Password=pwd;", connString);
		}
	}
}