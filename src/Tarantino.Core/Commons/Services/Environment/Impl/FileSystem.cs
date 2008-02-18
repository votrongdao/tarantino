using System;
using System.IO;
using StructureMap;

namespace Tarantino.Core.Commons.Services.Environment.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class FileSystem : IFileSystem
	{
		private IFileStreamFactory _streamFactory;

		public FileSystem(IFileStreamFactory streamFactory)
		{
			_streamFactory = streamFactory;
		}

		public void SaveFile(string filename, byte[] fileContent)
		{
			if (fileContent != null)
			{
				Stream fileStream = _streamFactory.ConstructWriteFileStream(filename);

				using (BinaryWriter writer = new BinaryWriter(fileStream))
				{
					writer.Write(fileContent);
				}
			}
		}

		public bool FileExists(string relativePath)
		{
			bool retval = File.Exists(relativePath);
			return retval;
		}

		public Stream ReadIntoFileStream(string path)
		{
			try
			{
				Stream stream = _streamFactory.ConstructReadFileStream(path);
				return stream;
			}
			catch (IOException ex)
			{
				if (ex.Message.IndexOf("it is being used by another process") >= 0)
				{
					throw new ApplicationException("The file you chose cannot be read because it is open in another application.  Please close the file in the other application and try again.");
				}
				else
					throw;
			}
		}

		public string[] GetAllFilesWithExtensionWithinFolder(string folder, string fileExtension)
		{
			string[] fileNames = new string[0];

			if (Directory.Exists(folder))
			{
				string searchPattern = string.Format("*.{0}", fileExtension);
				fileNames = Directory.GetFiles(folder, searchPattern, SearchOption.AllDirectories);
			}

			return fileNames;
		}

		public string ReadTextFile(string filename)
		{
			Stream stream = _streamFactory.ConstructReadFileStream(filename);
			using (StreamReader reader = new StreamReader(stream))
			{
				return reader.ReadToEnd();
			}
		}
	}
}