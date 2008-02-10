using System.IO;
using StructureMap;

namespace Tarantino.Commons.Core.Services.Environment
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IFileSystem
	{
		void SaveFile(string filename, byte[] fileContent);
		bool FileExists(string relativePath);
		Stream ReadIntoFileStream(string path);
	}
}