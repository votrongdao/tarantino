using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Tarantino.Core.DatabaseManager.Model;
using Tarantino.Core.DatabaseManager.Services;
using Tarantino.Core.DatabaseManager.Services.Impl;

namespace Tarantino.UnitTests.Core.DatabaseManager.Services
{
	[TestFixture]
	public class LogMessageGeneratorTester
	{
		[Test]
		public void Creates_initial_log_message_for_database_rebuild()
		{
			ILogMessageGenerator generator = new LogMessageGenerator();
            
			var settings = new ConnectionSettings("server", "db", true, null, null);
		    var taskAttributes = new TaskAttributes(settings, "c:\\scripts") {RequestedDatabaseAction = RequestedDatabaseAction.Rebuild};
			string message = generator.GetInitialMessage(taskAttributes);

			Assert.That(message, Is.EqualTo("Rebuild db on server using scripts from c:\\scripts\n"));
		}

		[Test]
		public void Creates_initial_log_message_for_database_create()
		{
			ILogMessageGenerator generator = new LogMessageGenerator();

			var settings = new ConnectionSettings("server", "db", true, null, null);
            var taskAttributes = new TaskAttributes(settings, "c:\\scripts");
			string message = generator.GetInitialMessage(taskAttributes);

			Assert.That(message, Is.EqualTo("Create db on server using scripts from c:\\scripts\n"));
		}

		[Test]
		public void Creates_initial_log_message_for_database_update()
		{
			ILogMessageGenerator generator = new LogMessageGenerator();

			var settings = new ConnectionSettings("server", "db", true, null, null);
            var taskAttributes = new TaskAttributes(settings, "c:\\scripts") {RequestedDatabaseAction = RequestedDatabaseAction.Update};
			string message = generator.GetInitialMessage(taskAttributes);

			Assert.That(message, Is.EqualTo("Update db on server using scripts from c:\\scripts\n"));
		}

		[Test]
		public void Creates_initial_log_message_for_database_drop()
		{
			ILogMessageGenerator generator = new LogMessageGenerator();

			var settings = new ConnectionSettings("server", "db", true, null, null);
            var taskAttributes = new TaskAttributes(settings, "c:\\scripts") {RequestedDatabaseAction = RequestedDatabaseAction.Drop};
			string message = generator.GetInitialMessage(taskAttributes);

			Assert.That(message, Is.EqualTo("Drop db on server\n"));
		}

        [Test]
        public void Creates_initial_log_message_for_database_create_while_skiping_some_files()
        {
            ILogMessageGenerator generator = new LogMessageGenerator();

            var settings = new ConnectionSettings("server", "db", true, null, null);
            var taskAttributes = new TaskAttributes(settings, "c:\\scripts") { SkipFileNameContaining = "_data_"};
            string message = generator.GetInitialMessage(taskAttributes);

            Assert.That(message, Is.EqualTo("Create db on server using scripts from c:\\scripts while skipping file containing _data_\n"));
        }
	}
}