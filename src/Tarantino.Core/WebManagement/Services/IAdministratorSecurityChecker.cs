using StructureMap;

namespace Tarantino.Core.WebManagement.Services
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IAdministratorSecurityChecker
	{
		bool IsCurrentUserAdministrator();
	}
}