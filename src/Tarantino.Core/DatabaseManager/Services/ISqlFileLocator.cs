using StructureMap;

namespace Tarantino.Core.DatabaseManager.Services
{
	[PluginFamily(Keys.Default)]
	public interface ISqlFileLocator
	{
		string[] GetSqlFilenames(string scriptBaseFolder, string scriptFolder);
	}
}