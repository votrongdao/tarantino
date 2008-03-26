using System.Security.Principal;
using Tarantino.Core.Commons.Services.Security;
using StructureMap;
using Tarantino.Core.Commons.Services.Web;

namespace Tarantino.Core.Commons.Services.Security.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class RoleAssigner : IRoleAssigner
	{
		private readonly IWebContext _context;
		private readonly IPrincipalFactory _principalFactory;

		public RoleAssigner(IWebContext context, IPrincipalFactory principalFactory)
		{
			_context = context;
			_principalFactory = principalFactory;
		}

		public void AssignCurrentUserToRoles(params string[] roles)
		{
			IIdentity userIdentity = _context.GetUserIdentity();
			IPrincipal principal = _principalFactory.CreatePrincipal(userIdentity, roles);
			_context.SetUser(principal);
		}
	}
}