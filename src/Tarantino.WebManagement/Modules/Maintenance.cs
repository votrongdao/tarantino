using System;
using StructureMap;
using Tarantino.Core.Commons.Services.Configuration;
using Tarantino.Core.WebManagement.Model;
using Tarantino.Core.WebManagement.Services.Repositories;

namespace Tarantino.WebManagement.Modules
{
	public class Maintenance : ModuleBase
	{
		private string _httphost = string.Empty;
		private string m_url = string.Empty;
		private bool m_filterByHost = false;
		private bool m_filterByExceptions = false;
		private string[] m_exts = new string[0];
		private string[] m_httphostExceptions = new string[0];

		public Maintenance()
		{
			IConfigurationReader reader = ObjectFactory.GetInstance<IConfigurationReader>();
		
			_httphost = reader.GetRequiredSetting("ApplicationManagementHttpHost").ToLower();

			string hostException = reader.GetRequiredSetting("applicationManagementHttpHostException").ToLower();

			if (hostException != null && hostException.Length > 0)
			{
				m_httphostExceptions = hostException.Split(',');
			}
			else
				m_httphostExceptions = new string[0];

			m_url = reader.GetRequiredSetting("applicationManagementMaintenancePage").ToLower();
			m_exts = reader.GetRequiredSetting("applicationManagementMaintenanceExts").ToLower().Split(',');

			Array.Sort(m_httphostExceptions);
			Array.Sort(m_exts);
			if (m_httphostExceptions.Length > 0)
			{
				m_filterByExceptions = true;
			}
			else if (_httphost.Length > 0)
			{
				m_filterByHost = true;
			}

			try
			{
				ApplicationInstance app = CurrentContext.CurrentApplicationInstance;
				if (_httphost.Length > 0)
				{
					IApplicationInstanceRepository repository = ObjectFactory.GetInstance<IApplicationInstanceRepository>();
					app.MaintenanceHostHeader = _httphost;
					repository.Save(app);
				}
			}
			catch { }
		}

		protected override void AuthenticateRequest(object sender, EventArgs e)
		{
			//check here for the url to redirect.  let .axd pass through.
			if (Array.BinarySearch(m_exts, System.IO.Path.GetExtension(m_context.Request.Url.AbsolutePath)) >= 0)
			{
				if (CurrentContext.CurrentApplicationInstance.DownForMaintenance)
				{
					if (
						(m_filterByHost && m_context.Request.ServerVariables["HTTP_HOST"] != null && _httphost == m_context.Request.ServerVariables["HTTP_HOST"].ToLower())
						||
						(m_filterByExceptions && m_context.Request.ServerVariables["HTTP_HOST"] != null && Array.BinarySearch(m_httphostExceptions, m_context.Request.ServerVariables["HTTP_HOST"].ToLower()) < 0)

						)
					{
						string url = m_url.ToLower().Replace("%locale%", System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName);
						m_context.Server.Transfer(url, false);
					}
				}
			}
		}

		protected override void AcquireRequestState(object sender, EventArgs e)
		{
		}

		protected override void BeginRequest(object sender, EventArgs e)
		{
		}
	}
}