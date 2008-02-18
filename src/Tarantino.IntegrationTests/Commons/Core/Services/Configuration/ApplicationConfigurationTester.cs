using System.Collections.Specialized;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Tarantino.Commons.Core.Services.Configuration;
using Tarantino.Commons.Core.Services.Configuration.Impl;

namespace Tarantino.IntegrationTests.Commons.Core.Services.Configuration
{
	[TestFixture]
	public class ApplicationConfigurationTester
	{
		[Test]
		public void Reads_application_setting()
		{
			IApplicationConfiguration settings = new ApplicationConfiguration();

			Assert.That(settings.GetSetting("MappingAssemblies"), Is.EqualTo("Tarantino.Infrastructure"));
		}

		[Test]
		public void Reads_missing_application_setting()
		{
			IApplicationConfiguration settings = new ApplicationConfiguration();

			Assert.That(settings.GetSetting("MissingSetting"), Is.Null);
		}

		[Test]
		public void Reads_database_connection_string()
		{
			IApplicationConfiguration settings = new ApplicationConfiguration();

			Assert.That(settings.GetConnectionString("DatabaseConnectionString"),
			            Is.EqualTo("data source=.;Initial Catalog=TarantinoDeployer;Integrated Security=true;Connect Timeout=100"));
		}

		[Test]
		public void Reads_configuration_section()
		{
			IApplicationConfiguration configuration = new ApplicationConfiguration();
			NameValueCollection settings = (NameValueCollection)configuration.GetSection("MySettings");

			Assert.That(settings.Keys, Is.EqualTo(new string[] { "key1", "key2" }));
			Assert.That(settings[0], Is.EqualTo("value1"));
			Assert.That(settings[1], Is.EqualTo("value2"));
		}

		[Test]
		public void Reads_missing_database_connection_string()
		{
			IApplicationConfiguration settings = new ApplicationConfiguration();

			Assert.That(settings.GetConnectionString("MissingConnectionString"), Is.Null);
		}
	}
}