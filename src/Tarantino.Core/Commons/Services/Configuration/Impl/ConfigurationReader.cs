using System;
using System.Collections.Generic;


namespace Tarantino.Core.Commons.Services.Configuration.Impl
{
	
	public class ConfigurationReader : IConfigurationReader
	{
		private readonly IApplicationConfiguration _settings;

		public ConfigurationReader(IApplicationConfiguration settings)
		{
			_settings = settings;
		}

		public string GetConnectionString(string key)
		{
			string connectionString = _settings.GetConnectionString(key);

			if (connectionString == null)
			{
				string message = string.Format("The database connection string '{0}' does not exist in the application configuration file.", key);
				throw new ApplicationException(message);
			}

			return connectionString;
		}

		public string GetRequiredSetting(string key)
		{
			string setting = _settings.GetSetting(key);

			if (setting == null)
			{
				string message = string.Format("The application setting '{0}' does not exist in the application configuration file.", key);
				throw new ApplicationException(message);
			}

			return setting;
		}

		public int GetRequiredIntegerSetting(string key)
		{
			string settingString = GetRequiredSetting(key);

			int setting;
			bool isInteger = int.TryParse(settingString, out setting);
			
			if (!isInteger)
			{
				string template = "The value for setting '{0}' ('{1}') is not an integer";
				throw new ApplicationException(string.Format(template, key, settingString));
			}

			return setting;
		}

		public bool GetRequiredBooleanSetting(string key)
		{
			string settingString = GetRequiredSetting(key);

			bool setting;
			bool isBoolean = bool.TryParse(settingString, out setting);

			if (!isBoolean)
			{
				string template = "The value for setting '{0}' ('{1}') is not a boolean";
				throw new ApplicationException(string.Format(template, key, settingString));
			}

			return setting;
		}

		public IEnumerable<string> GetStringArray(string key)
		{
			string setting = _settings.GetSetting(key);

			if (setting != null)
			{
				IEnumerable<string> rawSettings = setting.Split(',');

				foreach (string rawSetting in rawSettings)
				{
					yield return rawSetting.Trim();
				}
			}
		}

		public bool? GetOptionalBooleanSetting(string key)
		{
			string settingString = GetOptionalSetting(key);

			bool setting;
			bool isBoolean = bool.TryParse(settingString, out setting);
			bool? settingValue = !isBoolean ? null : new bool?(setting);
			return settingValue;
		}

		public string GetOptionalSetting(string key)
		{
			string setting = _settings.GetSetting(key);
			return setting;
		}
	}
}