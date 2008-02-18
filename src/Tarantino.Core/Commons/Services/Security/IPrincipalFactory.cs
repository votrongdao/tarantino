using System.Security.Principal;
using StructureMap;

namespace Tarantino.Core.Commons.Services.Security
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IPrincipalFactory
	{
		IPrincipal CreatePrincipal(IIdentity identity, params string[] roles);
	}
}