using System;
using System.IO;
using StructureMap;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Core.Commons.Services.Environment.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Tarantino.IntegrationTests.Core.Commons.Services.Environment
{
	[TestFixture]
	public class FileSystemTester
	{
		[Test]
		public void Correctly_Reports_File_Exists()
		{
			writeSampleFile();

			IFileSystem fileSystem = new FileSystem(null);

			Assert.AreEqual(true, fileSystem.FileExists("test.txt"));
			Assert.AreEqual(false, fileSystem.FileExists("nonExistentFile.txt"));
		}

		[Test, ExpectedException(ExceptionType = typeof(IOException), ExpectedMessage = "Not a locked file")]
		public void Handles_Reading_IO_Exceptions()
		{
			MockRepository mocks = new MockRepository();
			IFileStreamFactory streamFactory = mocks.CreateMock<IFileStreamFactory>();
			Expect.Call(streamFactory.ConstructReadFileStream(@"MyPath\test.txt")).Throw(new IOException("Not a locked file"));

			mocks.ReplayAll();

			IFileSystem fileSystem = new FileSystem(streamFactory);

			fileSystem.ReadIntoFileStream(@"MyPath\test.txt");
		}

		[Test, ExpectedException(ExceptionType = typeof(ApplicationException), ExpectedMessage = "The file you chose cannot be read because it is open in another application.  Please close the file in the other application and try again.")]
		public void Handles_Reading_Locked_Files()
		{
			MockRepository mocks = new MockRepository();
			IFileStreamFactory streamFactory = mocks.CreateMock<IFileStreamFactory>();
			Expect.Call(streamFactory.ConstructReadFileStream(@"MyPath\test.txt")).Throw(new IOException("it is being used by another process"));

			mocks.ReplayAll();

			IFileSystem fileSystem = new FileSystem(streamFactory);

			fileSystem.ReadIntoFileStream(@"MyPath\test.txt");
		}

		[Test]
		public void Correctly_Reads_File_Into_Stream()
		{
			MemoryStream myFileStream = new MemoryStream();

			MockRepository mocks = new MockRepository();
			IFileStreamFactory streamFactory = mocks.CreateMock<IFileStreamFactory>();
			Expect.Call(streamFactory.ConstructReadFileStream(@"MyPath\test.txt")).Return(myFileStream);

			mocks.ReplayAll();

			IFileSystem fileSystem = new FileSystem(streamFactory);

			using (Stream stream = fileSystem.ReadIntoFileStream(@"MyPath\test.txt"))
			{
				Assert.That(stream, Is.SameAs(myFileStream));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Correctly_Reads_Text_File()
		{
			writeSampleFile();
			IFileSystem fileSystem = ObjectFactory.GetInstance<IFileSystem>();
			string fileContents = fileSystem.ReadTextFile("test.txt");

			Assert.That(fileContents, Is.EqualTo("testing..."));
		}

		[Test]
		public void Correctly_finds_all_files_with_extension()
		{
			writeSampleFile();
			IFileSystem fileSystem = ObjectFactory.GetInstance<IFileSystem>();
			string[] files = fileSystem.GetAllFilesWithExtensionWithinFolder(".", "txt");
			
			Assert.That(files.Length, Is.EqualTo(1));
			Assert.That(files[0].Contains(@"\test.txt"));
		}

		[Test]
		public void Correctly_Writes_File()
		{
			deleteTestFile();
			FileStream myFileStream = new FileStream("test.txt", FileMode.Create, FileAccess.Write);
			byte[] fileContents = new byte[] { 1, 3, 5 };

			MockRepository mocks = new MockRepository();
			IFileStreamFactory streamFactory = mocks.CreateMock<IFileStreamFactory>();
			Expect.Call(streamFactory.ConstructWriteFileStream(@"myPath\test.txt")).Return(myFileStream);

			mocks.ReplayAll();

			IFileSystem fileSystem = new FileSystem(streamFactory);
			fileSystem.SaveFile(@"myPath\test.txt", fileContents);

			using (FileStream inputStream = new FileStream("test.txt", FileMode.Open, FileAccess.Read))
			{
				Assert.That(inputStream.Length, Is.EqualTo(3));
			}

			mocks.VerifyAll();
		}

		private void writeSampleFile()
		{
			deleteTestFile();

			using (StreamWriter streamWriter = new StreamWriter("test.txt"))
			{
				streamWriter.Write("testing...");
			}
		}

		private void deleteTestFile()
		{
			if (File.Exists("test.txt"))
				File.Delete("test.txt");
		}
	}
}