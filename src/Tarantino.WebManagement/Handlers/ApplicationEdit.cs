using System;
using StructureMap;
using Tarantino.Core.WebManagement.Model;
using Tarantino.Core.WebManagement.Services.Repositories;

namespace Tarantino.WebManagement.Handlers
{
	public class ApplicationEdit : HandlerBase
	{
		protected override void OnProcessRequest()
		{
			if (m_authenticated && m_context.Request["id"] != null)
			{
				Guid id = new Guid(m_context.Request["id"]);

				IApplicationInstanceRepository repository = ObjectFactory.GetInstance<IApplicationInstanceRepository>();
				ApplicationInstance applicationInstance = repository.GetById(id);

				if (m_context.Request.Form.Get("hostname") != null)
				{
					applicationInstance.UniqueHostHeader = m_context.Request.Form.Get("hostname");
					repository.Save(applicationInstance);
				}
				Write(ListApplications(applicationInstance));
			}

		}

		string ListApplications(ApplicationInstance applicationInstance)
		{
			System.Text.StringBuilder output = new System.Text.StringBuilder();
			WriteCSS();
			if (m_authenticated)
			{
				output.AppendFormat("<form method=post action=''>");
			}
			output.Append("<table >\n");
			output.AppendFormat("<tr><td><a href='callawaygolf.tx.web.management.application.axd'>Back</a></td><td></td></tr>");
			output.AppendFormat("<tr><td>Application</td><td>{0}</td></tr>", applicationInstance.ApplicationDomain);
			output.AppendFormat("<tr><td>Machine Name</td><td>{0}</td></tr>", applicationInstance.MachineName);
			output.AppendFormat("<tr><td>Version</td><td>{0}</td></tr>", applicationInstance.Version);
			output.AppendFormat("<tr><td>Unique Hostname</td><td><input type=\"text\" name=\"hostname\" value=\"{0}\"></td></tr>", applicationInstance.UniqueHostHeader);
			output.AppendFormat("<tr><td>Shared Hostname</td><td>{0}</td></tr>", applicationInstance.MaintenanceHostHeader);
			output.AppendFormat("<tr><td>Load balanaced</td><td>{0}</td></tr>", applicationInstance.AvailableForLoadBalancing ? "Online" : "Offline");
			output.AppendFormat("<tr><td>Maintenance Mode</td><td>{0}</td></tr>", applicationInstance.DownForMaintenance ? "Down" : "Online");
			output.Append("</table>\n");

			if (m_authenticated)
			{
				output.AppendFormat("<input type=submit value='Submit' />");
				output.AppendFormat("</form>");
			}

			return output.ToString();
		}
	}
}