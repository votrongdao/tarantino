using System.IO;

namespace Tarantino.DatabaseManager.NAntTasks.Services.Impl
{
	public class FileSystem : IFileSystem
	{
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
			using (StreamReader reader = new StreamReader(filename))
			{
				return reader.ReadToEnd();
			}
		}
	}
}