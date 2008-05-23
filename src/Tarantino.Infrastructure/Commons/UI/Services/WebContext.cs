using System;
using System.Security.Principal;
using System.Web;
using StructureMap;
using Tarantino.Core;
using Tarantino.Core.Commons.Services.Web;

namespace Tarantino.Infrastructure.Commons.UI.Services
{
	[Pluggable(Keys.Default)]
	public class WebContext : IWebContext
	{
		public bool UserIsAuthenticated()
		{
			bool isAuthenticated = HttpContext.Current.Request.IsAuthenticated;
			return isAuthenticated;
		}

		public string GetRequestItem(string key)
		{
			string value = HttpContext.Current.Request[key];
			return value;
		}

		public IIdentity GetUserIdentity()
		{
			IPrincipal user = HttpContext.Current.User;
			IIdentity identity = user != null ? user.Identity : null;
			return identity;
		}

		public IPrincipal GetUserPrinciple()
		{
			IPrincipal user = HttpContext.Current.User;
			return user;
		}

		public void SetItem(string key, object item)
		{
			HttpContext.Current.Items[key] = item;
		}

		public T GetItem<T>(string key)
		{
			object item = HttpContext.Current.Items[key];
			return (T)item;
		}

		public void SetSessionItem(string key, object item)
		{
			HttpContext.Current.Session[key] = item;
		}

		public bool HasSessionItem(string key)
		{
			return HttpContext.Current.Session[key] != null;
		}

		public T GetSessionItem<T>(string key)
		{
			object item = HttpContext.Current.Session[key];
			return (T)item;
		}

		public T GetCacheItem<T>(string key)
		{
			object item = HttpContext.Current.Cache[key];
			return (T) item;
		}

		public void SetCacheItem(string key, object item, DateTime expiration, TimeSpan slidingExpiration)
		{
			HttpContext.Current.Cache.Insert(key, item, null, expiration, slidingExpiration);
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

		public void SetHttpResponseStatus(int code, string description)
		{
			HttpResponse response = HttpContext.Current.Response;
			response.StatusCode = code;
			response.StatusDescription = description;
		}

		public void WriteToResponse(string message)
		{
			HttpContext.Current.Response.Write(message);
		}

		public void ServerTransfer(string url, bool preserveForm)
		{
			HttpContext.Current.Server.Transfer(url, preserveForm);
		}

		public string GetServerVariable(string variableName)
		{
			return HttpContext.Current.Request.ServerVariables[variableName];
		}
	}
}