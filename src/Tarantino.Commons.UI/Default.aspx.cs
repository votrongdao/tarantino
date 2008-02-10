using System;
using System.Web.UI;

namespace Tarantino.Commons.UI
{
	public partial class Default : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			lblUsername.Text = Context.User.Identity.Name;
		}
	}
}