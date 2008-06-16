using System.Collections.Generic;


namespace Tarantino.Core.Commons.Services.Configuration
{
	
	public interface IConfigurationReader
	{
		string GetConnectionString(string key);
		string GetRequiredSetting(string key);
		int GetRequiredIntegerSetting(string key);
		bool GetRequiredBooleanSetting(string key);
		IEnumerable<string> GetStringArray(string key);
		bool? GetOptionalBooleanSetting(string key);
		string GetOptionalSetting(string key);
	}
}