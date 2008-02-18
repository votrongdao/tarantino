using System.Configuration;
using StructureMap;

namespace Tarantino.Core.Commons.Services.Configuration.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class ApplicationConfiguration : IApplicationConfiguration
	{
		public string GetSetting(string settingName)
		{
			string settingValue = System.Configuration.ConfigurationManager.AppSettings[settingName];
			return settingValue;
		}

		public string GetConnectionString(string connectionStringName)
		{
			ConnectionStringSettings settings = System.Configuration.ConfigurationManager.ConnectionStrings[connectionStringName];
			string connectionString = settings != null ? settings.ConnectionString : null;
			return connectionString;
		}

		public object GetSection(string sectionName)
		{
			object section = System.Configuration.ConfigurationManager.GetSection(sectionName);
			return section;
		}
	}
}