using Tarantino.DatabaseManager.NAntTasks.Services.Impl;

namespace Tarantino.DatabaseManager.NAntTasks.Services
{
	public interface ISqlFileLocator
	{
		string[] GetSqlFilenames(string scriptFolder, DatabaseAction chosenAction);
	}
}