using NUnit.Framework;
using StructureMap;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.DatabaseManager.Services.Impl;

namespace Tarantino.IntegrationTests.Core.DatabaseManager
{
	[TestFixture]
	public class SqlFilesTester
	{
		[Test]
		public void All_sql_files_should_be_included_as_embedded_resources()
		{
			string assembly = DatabaseUpgrader.SQL_FILE_ASSEMBLY;
			string template = DatabaseUpgrader.SQL_FILE_TEMPLATE;

			IResourceFileLocator locator = ObjectFactory.GetInstance<IResourceFileLocator>();

			Assert.That(locator.FileExists(assembly, string.Format(template, "CreateSchema")));
			Assert.That(locator.FileExists(assembly, string.Format(template, "DropConnections")));
			Assert.That(locator.FileExists(assembly, string.Format(template, "VersionDatabase")));
		}
	}
}