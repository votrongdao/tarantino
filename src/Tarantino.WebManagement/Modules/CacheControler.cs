using System;
using System.Web;
using System.Configuration;
using System.Collections.Specialized;
using System.Web.Caching;

namespace Tarantino.WebManagement.Modules
{
	public class CacheControler : ModuleBase
	{
		protected static string[] domainNames = null;

		protected bool domainShouldBeCached = false;

		protected const string CacheDependencyKey = "OutputCacheDependency";

		protected override void Initialized()
		{
			if (domainNames == null)
			{
				NameValueCollection domainnames = (NameValueCollection)ConfigurationManager.GetSection("CacheControl/DomainNames");
				domainNames = new string[domainnames.Count];
				int index = 0;
				foreach (string domain in domainnames.Keys)
				{
					domainNames[index] = domain;
					index++;
				}
				Array.Sort(domainNames);
			}
			InsertCacheDependencyKey();
		}


		public virtual void Validate(HttpContext context, Object data, ref HttpValidationStatus status)
		{
			if (domainShouldBeCached)
				status = HttpValidationStatus.Valid;
			else
				status = HttpValidationStatus.IgnoreThisRequest;
		}

		private void SetDomainShouldBeCached()
		{
			string domain = m_context.Request.ServerVariables["HTTP_HOST"];
			domainShouldBeCached = (domain != null && Array.BinarySearch(domainNames, domain) >= 0);
		}

		private void InsertCacheDependencyKey()
		{
			HttpContext httpcontext = HttpContext.Current;
			if (httpcontext.Cache[CacheDependencyKey] == null)
			{
				httpcontext.Cache.Insert(CacheDependencyKey, DateTime.Now, null,
				                         DateTime.MaxValue, TimeSpan.Zero,
				                         CacheItemPriority.NotRemovable,
				                         null);
			}
		}

		protected override void BeginRequest(object sender, EventArgs e)
		{
			SetDomainShouldBeCached();

			InsertCacheDependencyKey();

			//callback to prevent showing cached content on non cached domain names
			m_context.Response.Cache.AddValidationCallback(new HttpCacheValidateHandler(Validate), null);

			//Do not cache the output of this request.
			if (m_context.Request.HttpMethod == "POST" || !domainShouldBeCached)
			{
				m_context.Response.Cache.SetNoServerCaching();
			}

			m_context.Response.AddCacheItemDependency(CacheDependencyKey);

		}

		protected override void AcquireRequestState(object sender, EventArgs e)
		{

		}
	}
}