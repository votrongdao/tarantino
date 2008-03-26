using StructureMap;

namespace Tarantino.Core.DatabaseManager.Services
{
	[PluginFamily(ServiceKeys.Default)]
	public interface ISqlFileLocator
	{
		string[] GetSqlFilenames(string scriptBaseFolder, string scriptFolder);
	}
}