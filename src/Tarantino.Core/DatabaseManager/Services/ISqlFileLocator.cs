using Tarantino.DatabaseManager.Services.Impl;

namespace Tarantino.DatabaseManager.Services
{
	public interface ISqlFileLocator
	{
		string[] GetSqlFilenames(string scriptFolder, DatabaseAction chosenAction);
	}
}