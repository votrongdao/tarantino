using StructureMap;

namespace Tarantino.Commons.Core.Services.Environment
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IResourceFileLocator
	{
		string ReadTextFile(string resourceName);
		byte[] ReadBinaryFile(string resourceName);
		bool FileExists(string resourceName);
	}
}