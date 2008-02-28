using System;
using System.Security.Principal;
using StructureMap;
using Tarantino.Core.WebManagement.Model;

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
		T GetCacheItem<T>(string key);
		void SetCacheItem(string key, object item, DateTime expiration, TimeSpan slidingExpiration);
	}
}