using System.Collections.Generic;
using System.IO;
using Tarantino.DatabaseManager.Services.Impl;

namespace Tarantino.DatabaseManager.Services.Impl
{
	public class SqlFileLocator : ISqlFileLocator
	{
		private IFileSystem _fileSystem;

		public SqlFileLocator(IFileSystem fileSystem)
		{
			_fileSystem = fileSystem;
		}

		private List<DatabaseAction> getDatabaseActions(DatabaseAction chosenAction)
		{
			List<DatabaseAction> list = new List<DatabaseAction>();

			if (chosenAction == DatabaseAction.Create || chosenAction == DatabaseAction.Rebuild)
			{
				list.Add(DatabaseAction.Create);
			}

			list.Add(DatabaseAction.Update);

			return list;
		}

		private string[] getSqlFilenames(string scriptFolder, DatabaseAction action)
		{
			string folder = Path.Combine(scriptFolder, action.ToString());

			return _fileSystem.GetAllFilesWithExtensionWithinFolder(folder, "sql");
		}

		public string[] GetSqlFilenames(string scriptFolder, DatabaseAction chosenAction)
		{
			List<string> list = new List<string>();

			foreach (DatabaseAction action in getDatabaseActions(chosenAction))
			{
				foreach (string sqlFilename in getSqlFilenames(scriptFolder, action))
				{
					list.Add(sqlFilename);
				}
			}

			return list.ToArray();
		}
	}
}