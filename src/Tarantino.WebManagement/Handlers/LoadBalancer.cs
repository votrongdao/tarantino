using System;
using Tarantino.Core.WebManagement.Model;

namespace Tarantino.WebManagement.Handlers
{
	public class LoadBalancer : HandlerBase
	{
		private string CurrentState
		{
			get
			{
				return string.Format("Load balancing has been {0} ", LoadBalanced ? "enabled" : "disabled");
			}
		}
		private bool LoadBalanced
		{
			get
			{
				ApplicationInstance a = ApplicationInstance.Current;

				return a.AvailableForLoadBalance;
			}
			set
			{
				ApplicationInstance a = ApplicationInstance.Current;
				a.AvailableForLoadBalance = value;
				a.AcceptChanges();
			}
		}

		private bool Enabled
		{
			get
			{
				if (Request("enabled").Length > 0)
					return bool.Parse(Request("enabled"));
				else
					return false;
			}
		}
		private bool ControlRequest
		{
			get
			{
				return Request("enabled").Length > 0;
			}
		}

		protected override void OnProcessRequest()
		{
			if (ControlRequest)
			{
				try
				{
					if (m_authenticated)
					{
						LoadBalanced = Enabled;
						m_context.Response.Redirect(m_context.Request.Url.AbsolutePath);
					}
					else
					{
						Write("Only authenticated requests can change that loadbalanced status.\n");
					}
				}
				catch (Exception ex)
				{
					Write(ex.ToString());
				}
			}
			else
			{
				try
				{
					if (!LoadBalanced)
					{
						m_context.Response.StatusCode = 400;
						m_context.Response.StatusDescription = "This application has been turned off";
					}
				}
				catch (Exception ex)
				{
					Write(ex.ToString());
					LoadBalanced = true;
				}
			}

			WriteCSS();
			WriteMenu();
			Write(CurrentState);

			Write(Environment.MachineName);

			if (m_authenticated)
			{
				Write("<br/><br/><a href=?enabled=true>enable</a>&nbsp;<a href=?enabled=false>disable</a>");
			}
		}
	}
}
