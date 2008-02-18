using System.Collections.Generic;
using StructureMap;

namespace Tarantino.Core.Commons.Services.Configuration
{
	[PluginFamily(ServiceKeys.Default)]
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