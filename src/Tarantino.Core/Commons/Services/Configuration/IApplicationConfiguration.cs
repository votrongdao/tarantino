using StructureMap;

namespace Tarantino.Core.Commons.Services.Configuration
{
	[PluginFamily(Keys.Default)]
	public interface IApplicationConfiguration
	{
		string GetSetting(string settingName);
		string GetConnectionString(string connectionStringName);
		object GetSection(string sectionName);
	}
}