using System.IO;
using StructureMap;

namespace Tarantino.Core.Commons.Services.Environment
{
	[PluginFamily(Keys.Default)]
	public interface IFileSystem
	{
		void SaveFile(string filename, byte[] fileContent);
		bool FileExists(string relativePath);
		Stream ReadIntoFileStream(string path);
		string[] GetAllFilesWithExtensionWithinFolder(string folder, string fileExtension);
		string ReadTextFile(string filename);
	}
}