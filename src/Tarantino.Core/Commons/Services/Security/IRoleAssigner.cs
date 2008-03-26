using StructureMap;

namespace Tarantino.Core.Commons.Services.Security
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IRoleAssigner
	{
		void AssignCurrentUserToRoles(params string[] roles);
	}
}