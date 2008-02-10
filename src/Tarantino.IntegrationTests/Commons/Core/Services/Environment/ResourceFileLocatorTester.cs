using System;
using Tarantino.Commons.Core.Services.Environment;
using Tarantino.Commons.Core.Services.Environment.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Tarantino.IntegrationTests.Commons.Core.Services.Environment
{
	[TestFixture]
	public class ResourceFileLocatorTester
	{
		[Test]
		public void CorrectlyReadsResourceFile()
		{
			IResourceFileLocator locator = new ResourceFileLocator();
			string contents = locator.ReadTextFile("Tarantino.Commons.Core.Services.DataFileManagement.Files.Sample.tab");

			Assert.IsTrue(contents.Contains("Tarantino"));
		}

		[Test]
		public void Correctly_Reports_When_Resource_File_Does_Not_Exist()
		{
			IResourceFileLocator locator = new ResourceFileLocator();
			bool exists = locator.FileExists("Tarantino.Commons.Core.Services.BadFile.tab");

			Assert.That(exists, Is.False);
		}

		[Test, ExpectedException(ExceptionType = typeof(ApplicationException), ExpectedMessage = "Resource file not found: BadResource.txt. Make sure the Build Action for the file is 'Embedded Resource'.")]
		public void CorrectlyThrowsExceptionWhenResourceNameNotFound()
		{
			IResourceFileLocator locator = new ResourceFileLocator();
			locator.ReadTextFile("BadResource.txt");
		}

		[Test]
		public void CorrectlyReadsBinaryResourceFile()
		{
			IResourceFileLocator locator = new ResourceFileLocator();
			string contents = locator.ReadTextFile("Tarantino.Commons.Core.Services.DataFileManagement.Files.Sample.tab");

			Assert.That(contents.Length, Is.GreaterThan(10));
		}

		[Test, ExpectedException(ExceptionType = typeof(ApplicationException), ExpectedMessage = "Resource file not found: BadResource.txt. Make sure the Build Action for the file is 'Embedded Resource'.")]
		public void CorrectlyThrowsExceptionWhenBinaryResourceNameNotFound()
		{
			IResourceFileLocator locator = new ResourceFileLocator();
			locator.ReadBinaryFile("BadResource.txt");
		}
	}
}