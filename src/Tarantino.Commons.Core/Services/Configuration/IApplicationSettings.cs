using System.Collections.Generic;
using StructureMap;

namespace Tarantino.Commons.Core.Services.Configuration
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IApplicationSettings
	{
		string GetConnectionString();

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