using System.Collections.Generic;
using System.IO;
using StructureMap;
using Tarantino.Core.Commons.Services.Environment;

namespace Tarantino.Core.DatabaseManager.Services.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class SqlFileLocator : ISqlFileLocator
	{
		private IFileSystem _fileSystem;

		public SqlFileLocator(IFileSystem fileSystem)
		{
			_fileSystem = fileSystem;
		}

		public string[] GetSqlFilenames(string scriptBaseFolder, string scriptFolder)
		{
			List<string> list = new List<string>();

			string folder = Path.Combine(scriptBaseFolder, scriptFolder);
			string[] sqlFiles = _fileSystem.GetAllFilesWithExtensionWithinFolder(folder, "sql");

			foreach (string sqlFilename in sqlFiles)
			{
				list.Add(sqlFilename);
			}

			return list.ToArray();
		}
	}
}