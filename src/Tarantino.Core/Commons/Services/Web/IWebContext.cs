using System.Security.Principal;
using StructureMap;

namespace Tarantino.Core.Commons.Services.Web
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IWebContext
	{
		bool UserIsAuthenticated();
		IIdentity GetUserIdentity();
		void SetUser(IPrincipal user);
		void Redirect(string url);
		void SetItem(string key, object item);
		T GetItem<T>(string key);
		void RewriteUrl(string newUrl);
		string GetCurrentUrl();
	}
}