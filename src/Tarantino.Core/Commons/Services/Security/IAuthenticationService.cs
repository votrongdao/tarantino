
using Tarantino.Core;

namespace Tarantino.Core.Commons.Services.Security
{
	
	public interface IAuthenticationService
	{
		void RedirectFromLoginPage(string emailAddress, bool rememberMe);
		void Logout();
		string GetLoginUrl();
	}
}