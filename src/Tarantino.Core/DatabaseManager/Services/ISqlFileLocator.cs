using StructureMap;
using Tarantino.Core.DatabaseManager.Services.Impl;

namespace Tarantino.Core.DatabaseManager.Services
{
	[PluginFamily(ServiceKeys.Default)]
	public interface ISqlFileLocator
	{
		string[] GetSqlFilenames(string scriptFolder, DatabaseAction chosenAction);
	}
}