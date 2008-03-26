using System.Security.Principal;
using StructureMap;

namespace Tarantino.Core.Commons.Services.Security.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class PrincipalFactory : IPrincipalFactory
	{
		public IPrincipal CreatePrincipal(IIdentity identity, params string[] roles)
		{
			GenericPrincipal principal = new GenericPrincipal(identity, roles);
			return principal;
		}
	}
}