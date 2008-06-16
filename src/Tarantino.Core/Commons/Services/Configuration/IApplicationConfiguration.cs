

namespace Tarantino.Core.Commons.Services.Configuration
{
	
	public interface IApplicationConfiguration
	{
		string GetSetting(string settingName);
		string GetConnectionString(string connectionStringName);
		object GetSection(string sectionName);
	}
}