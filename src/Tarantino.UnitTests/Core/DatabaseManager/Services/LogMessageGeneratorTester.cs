using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Tarantino.Core.DatabaseManager.Services;
using Tarantino.Core.DatabaseManager.Services.Impl;
using Tarantino.DatabaseManager.Model;

namespace Tarantino.UnitTests.Core.DatabaseManager.Services
{
	[TestFixture]
	public class LogMessageGeneratorTester
	{
		[Test]
		public void Creates_initial_log_message_for_database_rebuild()
		{
			ILogMessageGenerator generator = new LogMessageGenerator();

			ConnectionSettings settings = new ConnectionSettings("server", "db", true, null, null);

			string message = generator.GetInitialMessage(RequestedDatabaseAction.Rebuild, "c:\\scripts", settings);

			Assert.That(message, Is.EqualTo("Rebuild db on server using scripts from c:\\scripts\n"));
		}

		[Test]
		public void Creates_initial_log_message_for_database_create()
		{
			ILogMessageGenerator generator = new LogMessageGenerator();

			ConnectionSettings settings = new ConnectionSettings("server", "db", true, null, null);

			string message = generator.GetInitialMessage(RequestedDatabaseAction.Create, "c:\\scripts", settings);

			Assert.That(message, Is.EqualTo("Create db on server using scripts from c:\\scripts\n"));
		}

		[Test]
		public void Creates_initial_log_message_for_database_update()
		{
			ILogMessageGenerator generator = new LogMessageGenerator();

			ConnectionSettings settings = new ConnectionSettings("server", "db", true, null, null);

			string message = generator.GetInitialMessage(RequestedDatabaseAction.Update, "c:\\scripts", settings);

			Assert.That(message, Is.EqualTo("Update db on server using scripts from c:\\scripts\n"));
		}

		[Test]
		public void Creates_initial_log_message_for_database_drop()
		{
			ILogMessageGenerator generator = new LogMessageGenerator();

			ConnectionSettings settings = new ConnectionSettings("server", "db", true, null, null);

			string message = generator.GetInitialMessage(RequestedDatabaseAction.Drop, null, settings);

			Assert.That(message, Is.EqualTo("Drop db on server\n"));
		}
	}
}