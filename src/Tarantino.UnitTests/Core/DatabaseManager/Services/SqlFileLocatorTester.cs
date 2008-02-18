using Tarantino.Core.Commons.Services.Environment;
using Tarantino.DatabaseManager.Services;
using Tarantino.DatabaseManager.Services.Impl;
using NUnit.Framework;
using Rhino.Mocks;

namespace Tarantino.UnitTests.Core.DatabaseManager.Services
{
	[TestFixture]
	public class SqlFileLocatorTester
	{
		[Test]
		public void CorrectlyLocatesUpdateSqlScripts()
		{
			string scriptFolder = @"c:\scripts";

			string[] sqlFilesInFolder = new string[] {"01_Test.sql", "02_Test.sql"};

			MockRepository mocks = new MockRepository();
			IFileSystem fileSystem = mocks.CreateMock<IFileSystem>();
			Expect.Call(fileSystem.GetAllFilesWithExtensionWithinFolder(@"c:\scripts\Update", "sql")).Return(sqlFilesInFolder);

			mocks.ReplayAll();

			ISqlFileLocator fileLocator = new SqlFileLocator(fileSystem);
			string[] sqlFilenames = fileLocator.GetSqlFilenames(scriptFolder, DatabaseAction.Update);

			Assert.AreEqual(2, sqlFilenames.Length);
			Assert.AreEqual("01_Test.sql", sqlFilenames[0]);
			Assert.AreEqual("02_Test.sql", sqlFilenames[1]);

			mocks.VerifyAll();
		}

		[Test]
		public void CorrectlyLocatesCreateSqlScripts()
		{
			string scriptFolder = @"c:\scripts";

			string[] createSqlFiles = new string[] { "01_Create.sql", "02_Create.sql" };
			string[] updateSqlFiles = new string[] { "01_Update.sql", "02_Update.sql" };

			MockRepository mocks = new MockRepository();
			IFileSystem fileSystem = mocks.CreateMock<IFileSystem>();
			Expect.Call(fileSystem.GetAllFilesWithExtensionWithinFolder(@"c:\scripts\Create", "sql")).Return(createSqlFiles);
			Expect.Call(fileSystem.GetAllFilesWithExtensionWithinFolder(@"c:\scripts\Update", "sql")).Return(updateSqlFiles);

			mocks.ReplayAll();

			ISqlFileLocator fileLocator = new SqlFileLocator(fileSystem);
			string[] sqlFilenames = fileLocator.GetSqlFilenames(scriptFolder, DatabaseAction.Create);

			Assert.AreEqual(4, sqlFilenames.Length);
			Assert.AreEqual("01_Create.sql", sqlFilenames[0]);
			Assert.AreEqual("02_Create.sql", sqlFilenames[1]);
			Assert.AreEqual("01_Update.sql", sqlFilenames[2]);
			Assert.AreEqual("02_Update.sql", sqlFilenames[3]);

			mocks.VerifyAll();
		}

		[Test]
		public void CorrectlyLocatesCreateSqlScriptsOnRebuild()
		{
			string scriptFolder = @"c:\scripts";

			string[] createSqlFiles = new string[] { "01_Create.sql", "02_Create.sql" };
			string[] updateSqlFiles = new string[] { "01_Update.sql", "02_Update.sql" };

			MockRepository mocks = new MockRepository();
			IFileSystem fileSystem = mocks.CreateMock<IFileSystem>();
			Expect.Call(fileSystem.GetAllFilesWithExtensionWithinFolder(@"c:\scripts\Create", "sql")).Return(createSqlFiles);
			Expect.Call(fileSystem.GetAllFilesWithExtensionWithinFolder(@"c:\scripts\Update", "sql")).Return(updateSqlFiles);

			mocks.ReplayAll();

			ISqlFileLocator fileLocator = new SqlFileLocator(fileSystem);
			string[] sqlFilenames = fileLocator.GetSqlFilenames(scriptFolder, DatabaseAction.Rebuild);

			Assert.AreEqual(4, sqlFilenames.Length);
			Assert.AreEqual("01_Create.sql", sqlFilenames[0]);
			Assert.AreEqual("02_Create.sql", sqlFilenames[1]);
			Assert.AreEqual("01_Update.sql", sqlFilenames[2]);
			Assert.AreEqual("02_Update.sql", sqlFilenames[3]);

			mocks.VerifyAll();
		}
	}
}