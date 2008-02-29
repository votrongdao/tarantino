using StructureMap;
using Tarantino.Core.Commons.Services.Security;
using Tarantino.Core.Commons.Services.Web;

namespace Tarantino.Core.WebManagement.Services.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class AdministratorSecurityChecker : IAdministratorSecurityChecker
	{
		private readonly IWebContext _context;
		private readonly IRoleManager _manager;

		public AdministratorSecurityChecker(IWebContext context, IRoleManager manager)
		{
			_context = context;
			_manager = manager;
		}

		public bool IsCurrentUserAdministrator()
		{
			bool isAdministrator = _manager.IsCurrentUserInAtLeastOneRole(@"BUILTIN\Administrators", "Administrators");
			bool isAuthenticatedAdministrator = _context.UserIsAuthenticated() && isAdministrator;

			return isAuthenticatedAdministrator;
		}
	}
}