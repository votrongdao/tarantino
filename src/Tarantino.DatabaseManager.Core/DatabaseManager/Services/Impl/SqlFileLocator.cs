using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tarantino.DatabaseManager.Core;

namespace Tarantino.Core.DatabaseManager.Services.Impl
{
	
	public class SqlFileLocator : ISqlFileLocator
	{
		private IFileSystem _fileSystem;

		public SqlFileLocator(IFileSystem fileSystem)
		{
			_fileSystem = fileSystem;
		}

	    public SqlFileLocator():this(new FileSystem())
	    {
	        
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
		    return list.OrderBy(x => x).ToArray();
		}
	}
}