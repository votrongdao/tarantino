using StructureMap;

namespace Tarantino.Commons.Core.Services.Configuration
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IApplicationConfiguration
	{
		string GetSetting(string settingName);
		string GetConnectionString(string connectionStringName);
		object GetSection(string sectionName);
	}
}