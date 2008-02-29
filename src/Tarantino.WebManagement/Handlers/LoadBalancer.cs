using System.Web;
using StructureMap;
using Tarantino.Core.WebManagement.Services;

namespace Tarantino.WebManagement.Handlers
{
	public class LoadBalancer : IHttpHandler
	{
		public void ProcessRequest(HttpContext doNotUse)
		{
			IExceptionHandlingLoadBalanceStatusManager manager = ObjectFactory.GetInstance<IExceptionHandlingLoadBalanceStatusManager>();
			manager.HandleLoadBalancing();

			//WriteCSS();
			//WriteMenu();

			//string state = shouldBeOnline ? "enabled" : "disabled";
			//Write(string.Format("Load balancing has been {0} ", state));

			//Write(Environment.MachineName);

			//if (authenticated)
			//{
			//  Write("<br/><br/><a href=?enabled=true>enable</a>&nbsp;<a href=?enabled=false>disable</a>");
			//}
		}

		public bool IsReusable
		{
			get { return false; }
		}
	}
}