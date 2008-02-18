using System.Security.Principal;
using System.Web;
using StructureMap;
using Tarantino.Core;
using Tarantino.Core.Commons.Services.Web;

namespace Tarantino.Infrastructure.Commons.UI.Services
{
	[Pluggable(ServiceKeys.Default)]
	public class WebContext : IWebContext
	{
		public bool UserIsAuthenticated()
		{
			bool isAuthenticated = HttpContext.Current.Request.IsAuthenticated;
			return isAuthenticated;
		}

		public IIdentity GetUserIdentity()
		{
			IPrincipal user = HttpContext.Current.User;
			IIdentity identity = user != null ? user.Identity : null;
			return identity;
		}

		public void SetItem(string key, object item)
		{
			HttpContext.Current.Items[key] = item;
		}

		public T GetItem<T>(string key)
		{
			object item = HttpContext.Current.Items[key];
			return (T) item;
		}

		public void RewriteUrl(string newUrl)
		{
			HttpContext.Current.RewritePath(newUrl);
		}

		public void Redirect(string url)
		{
			HttpContext.Current.Response.Redirect(url);
		}

		public void SetUser(IPrincipal user)
		{
			HttpContext.Current.User = user;
		}
		
		public string GetCurrentUrl()
		{
			string url = HttpContext.Current.Request.Path;
			return url;
		}
	}
}