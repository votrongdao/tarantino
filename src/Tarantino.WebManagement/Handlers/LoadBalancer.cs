using System;
using System.Web;
using StructureMap;
using Tarantino.Core.WebManagement.Model;
using Tarantino.Core.WebManagement.Services.Repositories;




//namespace Tarantino.WebManagement.Handlers
//{
//  public class LoadBalanacerController
//  {
//    public void 
//  }
//}

namespace Tarantino.WebManagement.Handlers
{
	public class LoadBalancer : IHttpHandler
	{
		private bool _authenticated;

		public void ProcessRequest(HttpContext context)
		{
			if (context.Request.IsAuthenticated && (context.User.IsInRole(@"BUILTIN\Administrators") || context.User.IsInRole(@"Administrators")))
			{
				_authenticated = true;
			}

			OnProcessRequest();
		}

		public bool IsReusable
		{
			get { return false; }
		}

		protected string Request(string key)
		{
			if (HttpContext.Current.Request[key] != null)
			{
				return HttpContext.Current.Request[key].ToString();
			}
			else
			{
				return string.Empty;
			}
		}

		protected void OnProcessRequest()
		{
			//ILoadBalancer balancer = ObjectFactory.GetInstance<ILoadBalancer>();
			//balancer.Balance();
			bool shouldBeOnline = CurrentContext.CurrentApplicationInstance.AvailableForLoadBalancing;
			if (Request("enabled").Length > 0)
			{
				try
				{
					if (_authenticated)
					{
						bool enabled = (Request("enabled").Length > 0) ? bool.Parse(Request("enabled")) : false;

						setLoadBalanced(enabled);
						HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.AbsolutePath);
					}
					else
					{
						// Return this string
						//Write("Only authenticated requests can change that loadbalanced status.\n");
					}
				}
				catch (Exception ex)
				{
					//Return this string
					//Write(ex.ToString());
				}
			}
			else
			{
				try
				{
					if (!shouldBeOnline)
					{
						HttpContext.Current.Response.StatusCode = 400;
						HttpContext.Current.Response.StatusDescription = "This application has been turned off";
					}
				}
				catch (Exception ex)
				{
					//Return this string
					//Write(ex.ToString());
				}
			}

			//WriteCSS();
			//WriteMenu();

			//string state = shouldBeOnline ? "enabled" : "disabled";
			//Write(string.Format("Load balancing has been {0} ", state));

			//Write(Environment.MachineName);

			//if (_authenticated)
			//{
			//  Write("<br/><br/><a href=?enabled=true>enable</a>&nbsp;<a href=?enabled=false>disable</a>");
			//}
		}

		private void setLoadBalanced(bool value)
		{
			ApplicationInstance a = CurrentContext.CurrentApplicationInstance;
			a.AvailableForLoadBalancing = value;

			IApplicationInstanceRepository repository = ObjectFactory.GetInstance<IApplicationInstanceRepository>();
			repository.Save(a);
		}
	}
}