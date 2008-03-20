using StructureMap;
using Tarantino.Core;

namespace Tarantino.Core.Commons.Services.Security
{
	[PluginFamily(Keys.Default)]
	public interface IAuthenticationService
	{
		void RedirectFromLoginPage(string emailAddress, bool rememberMe);
		void Logout();
		string GetLoginUrl();
	}
}