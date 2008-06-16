using System.Configuration;


namespace Tarantino.Core.Commons.Services.Configuration.Impl
{
	
	public class ApplicationConfiguration : IApplicationConfiguration
	{
		public string GetSetting(string settingName)
		{
			var settingValue = ConfigurationManager.AppSettings[settingName];
			return settingValue;
		}

		public string GetConnectionString(string connectionStringName)
		{
			var settings = ConfigurationManager.ConnectionStrings[connectionStringName];
			var connectionString = settings != null ? settings.ConnectionString : null;
			return connectionString;
		}

		public object GetSection(string sectionName)
		{
			var section = ConfigurationManager.GetSection(sectionName);
			return section;
		}
	}
}