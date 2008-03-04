using System.Collections.Generic;
using StructureMap;

namespace Tarantino.Core.Commons.Services.Configuration
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IApplicationSettings
	{
		int GetServiceSleepTime();

		string GetSmtpServer();
		string GetSmtpUsername();
		string GetSmtpPassword();
		bool GetSmtpAuthenticationNecessary();
		IEnumerable<string> GetMappingAssemblies();
		bool GetShowSql();
		string GetServiceAgentFactory();
	}
}