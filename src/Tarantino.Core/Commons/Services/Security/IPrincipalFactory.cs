using System.Security.Principal;
using StructureMap;

namespace Tarantino.Core.Commons.Services.Security
{
	[PluginFamily(Keys.Default)]
	public interface IPrincipalFactory
	{
		IPrincipal CreatePrincipal(IIdentity identity, params string[] roles);
	}
}