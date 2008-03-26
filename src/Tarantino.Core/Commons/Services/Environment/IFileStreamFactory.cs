using System.IO;
using StructureMap;

namespace Tarantino.Core.Commons.Services.Environment
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IFileStreamFactory
	{
		Stream ConstructReadFileStream(string path);
		Stream ConstructWriteFileStream(string path);
	}
}