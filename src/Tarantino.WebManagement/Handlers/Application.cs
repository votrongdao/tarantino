using System;
using System.Collections.Generic;
using StructureMap;
using Tarantino.Core.Commons.Services.Web;
using Tarantino.Core.WebManagement.Model;
using Tarantino.Core.WebManagement.Services.Repositories;

namespace Tarantino.WebManagement.Handlers
{
	public class Application : HandlerBase
	{
		protected override void OnProcessRequest()
		{
			if (m_authenticated && m_context.Request["id"] != null && m_context.Request["a"] != null && m_context.Request["value"] != null)
			{
				Guid id = new Guid(m_context.Request["id"]);

				IApplicationInstanceRepository repository = ObjectFactory.GetInstance<IApplicationInstanceRepository>();
				ApplicationInstance instance = repository.GetById(id);

				bool boolValue = bool.Parse(m_context.Request["value"]);

				switch (m_context.Request["a"])
				{
					case "DownForMaintenance":
						SetAllMaintenance(instance, boolValue);
						break;
					case "AvailableForLoadBalance":
						instance.AvailableForLoadBalancing = boolValue;
						repository.Save(instance);
						break;
					case "Edit":
						break;
					case "Delete":
						repository.Delete(instance);
						break;
					case "Cache":
						try
						{
							if (instance.UniqueHostHeader != string.Empty)
							{
								IWebDataReader reader = ObjectFactory.GetInstance<IWebDataReader>();
								string cacheKey = CurrentContext.CacheKey;
								string url = string.Format("http://{0}/{1}?pattern={2}", "Tarantino.WebManagement.cache.axd", instance.UniqueHostHeader, cacheKey);
								reader.ReadUrl(url);
							}
						}
						catch
						{
						}

						break;
				}
				
				Reload();
			}
			
			WriteCSS();
			WriteMenu();
			
			Write(ListApplications());
		}

		private void SetAllMaintenance(ApplicationInstance ai, bool value)
		{
			IApplicationInstanceRepository repository = ObjectFactory.GetInstance<IApplicationInstanceRepository>();
			IEnumerable<ApplicationInstance> applicationInstances = repository.GetByHostHeader(ai.UniqueHostHeader);
			foreach (ApplicationInstance applicationInstance in applicationInstances)
			{
				if (applicationInstance.MaintenanceHostHeader == ai.MaintenanceHostHeader)
				{
					applicationInstance.DownForMaintenance = value;
					repository.Save(applicationInstance);
				}
			}
		}

		private string ListApplications()
		{
			System.Text.StringBuilder output = new System.Text.StringBuilder();

			IApplicationInstanceRepository repository = ObjectFactory.GetInstance<IApplicationInstanceRepository>();
			IList<ApplicationInstance> applications = new List<ApplicationInstance>(repository.GetAll());

			WriteCSS();

			output.Append("<table>\n");
			if (applications.Count > 0)
			{
				output.AppendFormat("<tr ><th>{0}</th><th>{1}</th><th>{2}</th><th class='center'>{3}</th><th class='center'>{4}</th><th>{5}</th>", "MaintenanceHostHeader", "Maintenance Mode", "MachineName", "Load balanaced", "Version", "Unique Hostname");

				if (m_authenticated)
					output.AppendFormat("<th colspan=3>Action</th>");

				output.AppendFormat("</tr>\r");
				string lastHost = "";
				foreach (ApplicationInstance ai in applications)
				{
					output.AppendFormat("<tr onMouseOver=\"className='over';\" onMouseOut=\"className='out';\"	 >");

					if (lastHost != ai.MaintenanceHostHeader)
					{
						int appcount = ApplicationCount(applications, ai.MaintenanceHostHeader);
						output.AppendFormat("<td rowspan='{9}' class='out'><a target='_blank' href='http://{1}'>{1}</a></td>", ai.MachineName, ai.MaintenanceHostHeader != lastHost ? ai.MaintenanceHostHeader : "", ai.Version, ai.DownForMaintenance ? "Down" : "Online", ai.AvailableForLoadBalancing ? "Online" : "Offline", ai.UniqueHostHeader, ai.Id, !ai.DownForMaintenance, !ai.AvailableForLoadBalancing, appcount);

						if (m_authenticated)
						{
							output.AppendFormat("<td rowspan='{9}' class='{3}'><a class='{3}' href='?id={6}&a=DownForMaintenance&value={7}'>{3}</a></td>", ai.MachineName, ai.MaintenanceHostHeader, ai.Version, ai.DownForMaintenance ? "Down" : "Online", ai.AvailableForLoadBalancing ? "Online" : "Offline", ai.UniqueHostHeader, ai.Id, !ai.DownForMaintenance, !ai.AvailableForLoadBalancing, appcount);
						}
						else
						{
							output.AppendFormat("<td rowspan='{9}' class='{3}'>{3}</td>", ai.MachineName, ai.MaintenanceHostHeader, ai.Version, ai.DownForMaintenance ? "Down" : "Online", ai.AvailableForLoadBalancing ? "Online" : "Offline", ai.UniqueHostHeader, ai.Id, !ai.DownForMaintenance, !ai.AvailableForLoadBalancing, appcount);
						}
					}

					output.AppendFormat("<td>{0}</td>", ai.MachineName, ai.MaintenanceHostHeader != lastHost ? ai.MaintenanceHostHeader : "", ai.Version, ai.DownForMaintenance ? "Down" : "Online", ai.AvailableForLoadBalancing ? "Online" : "Offline", ai.UniqueHostHeader, ai.Id, !ai.DownForMaintenance, !ai.AvailableForLoadBalancing);

					if (m_authenticated)
					{
						output.AppendFormat("<td class='{4}' ><a class='{4}' href='?id={6}&a=AvailableForLoadBalance&value={8}'>{4}</a></td>", ai.MachineName, ai.MaintenanceHostHeader, ai.Version, ai.DownForMaintenance ? "Down" : "Online", ai.AvailableForLoadBalancing ? "Online" : "Offline", ai.UniqueHostHeader, ai.Id, !ai.DownForMaintenance, !ai.AvailableForLoadBalancing);
					}
					else
					{
						output.AppendFormat("<td class='{4}' >{4}</td>", ai.MachineName, ai.MaintenanceHostHeader, ai.Version, ai.DownForMaintenance ? "Down" : "Online", ai.AvailableForLoadBalancing ? "Online" : "Offline", ai.UniqueHostHeader, ai.Id, !ai.DownForMaintenance, !ai.AvailableForLoadBalancing);
					}

					output.AppendFormat("<td>{2}</td>", ai.MachineName, ai.MaintenanceHostHeader != lastHost ? ai.MaintenanceHostHeader : "", ai.Version, ai.DownForMaintenance ? "Down" : "Online", ai.AvailableForLoadBalancing ? "Online" : "Offline", ai.UniqueHostHeader, ai.Id, !ai.DownForMaintenance, !ai.AvailableForLoadBalancing);

					output.AppendFormat("<td><a target='_blank' href='http://{5}'>{5}</a></td>", ai.MachineName, ai.MaintenanceHostHeader, ai.Version, ai.DownForMaintenance ? "Down" : "Online", ai.AvailableForLoadBalancing ? "Online" : "Offline", ai.UniqueHostHeader, ai.Id, !ai.DownForMaintenance, !ai.AvailableForLoadBalancing);

					if (m_authenticated)
					{
						output.AppendFormat("<td>");
						if (ai.UniqueHostHeader != null && ai.UniqueHostHeader.Length > 0)
						{
							output.AppendFormat("&nbsp;<a title=\"Refresh Cache\" href=\"?a=Cache&id={6}&value=true\">Refresh</a>&nbsp;",
							                    ai.MachineName, ai.MaintenanceHostHeader, ai.Version, ai.DownForMaintenance ? "Down" : "Online", ai.AvailableForLoadBalancing ? "Online" : "Offline", ai.UniqueHostHeader, ai.Id, !ai.DownForMaintenance, !ai.AvailableForLoadBalancing);
						}

						output.AppendFormat("</td>");
						output.AppendFormat("<td>");
						output.AppendFormat("&nbsp;<a href=\"callawaygolf.tx.web.management.applicationedit.axd?id={6}\">Edit</a>&nbsp;", ai.MachineName, ai.MaintenanceHostHeader, ai.Version, ai.DownForMaintenance ? "Down" : "Online", ai.AvailableForLoadBalancing ? "Online" : "Offline", ai.UniqueHostHeader, ai.Id, !ai.DownForMaintenance, !ai.AvailableForLoadBalancing);
						output.AppendFormat("</td>");
						output.AppendFormat("<td>");
						output.AppendFormat("&nbsp;<a href=\"?a=Delete&id={6}&value=true\">Delete</a>&nbsp;</td>", ai.MachineName, ai.MaintenanceHostHeader, ai.Version, ai.DownForMaintenance ? "Down" : "Online", ai.AvailableForLoadBalancing ? "Online" : "Offline", ai.UniqueHostHeader, ai.Id, !ai.DownForMaintenance, !ai.AvailableForLoadBalancing);
						output.AppendFormat("</td>");
					}

					output.AppendFormat("</tr>\r");
					lastHost = ai.MaintenanceHostHeader;
				}
			}
			else
			{
				output.Append("<tr><td>There are no applications</td></tr>\n");
			}
			output.Append("</table>\n");
			return output.ToString();
		}

		private int ApplicationCount(IEnumerable<ApplicationInstance> apps, string HostName)
		{
			int count = 0;
			foreach (ApplicationInstance ai in apps)
			{
				if (HostName != null && ai.MaintenanceHostHeader != null && ai.MaintenanceHostHeader.Equals(HostName))
					count++;
			}
			return count < 1 ? 1 : count;
		}
	}
}