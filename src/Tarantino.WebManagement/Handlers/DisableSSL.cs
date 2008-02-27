namespace Tarantino.WebManagement.Handlers
{
	/// <summary>
	/// Summary description for DisableSSL.
	/// </summary>
	public class DisableSSL : HandlerBase
	{
		protected override void OnProcessRequest()
		{
			WriteCSS();
			WriteMenu();

			if (m_context.Request["bypass"] != null)
			{
				if (m_context.Request["bypass"].ToLower().Equals("true"))
				{
					if (m_context.Response.Cookies["bypassssl"] != null)
					{
						m_context.Response.Cookies.Remove("bypassssl");
					}
					m_context.Response.Cookies.Add(new System.Web.HttpCookie("bypassssl"));
					m_context.Response.Cookies["bypassssl"].Value = "true";
				}
				else
				{
					m_context.Response.Cookies["bypassssl"].Value = "false";
				}
			}
			if (m_context.Response.Cookies["bypassssl"] != null && m_context.Response.Cookies["bypassssl"].Value != null && m_context.Response.Cookies["bypassssl"].Value.ToLower() == "true")
			{
				Write("SSL Disabled</br>");
			}
			else
			{
				Write("SSL Enabled</br>");
			}
			Write("<a href='?bypass=true'>Bypass</a></br>");
			Write("<a href='?bypass=false'>Enable</a>");
		}
	}
}
