using StructureMap;

namespace Tarantino.Core.Commons.Services.Security
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IRoleManager
	{
		void AssignCurrentUserToRoles(params string[] roles);
		bool IsCurrentUserInRole(string role);
		bool IsCurrentUserInAtLeastOneRole(params string[] roles);
	}
}