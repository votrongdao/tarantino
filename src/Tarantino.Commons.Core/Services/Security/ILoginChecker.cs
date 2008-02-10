using StructureMap;

namespace Tarantino.Commons.Core.Services.Security
{
	[PluginFamily(ServiceKeys.Default)]
	public interface ILoginChecker
	{
		bool IsValidUser(string emailAddress, string clearTextPassword);
	}
}