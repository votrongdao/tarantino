using System.IO;
using StructureMap;

namespace Tarantino.Commons.Core.Services.Environment.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class FileStreamFactory : IFileStreamFactory
	{
		public Stream ConstructReadFileStream(string path)
		{
			FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
			return stream;
		}

		public Stream ConstructWriteFileStream(string path)
		{
			FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
			return stream;
		}
	}
}