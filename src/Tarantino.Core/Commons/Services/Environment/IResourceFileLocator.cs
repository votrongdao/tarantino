using StructureMap;

namespace Tarantino.Core.Commons.Services.Environment
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IResourceFileLocator
	{
		string ReadTextFile(string assembly, string resourceName);
		byte[] ReadBinaryFile(string assembly, string resourceName);
		bool FileExists(string assembly, string resourceName);
	}
}