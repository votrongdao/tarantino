using System;
using System.Net.Mail;
using System.Web.Security;
using System.Web.UI;
using Tarantino.Commons.Core.Services.Net;
using StructureMap;

namespace Tarantino.Commons.UI
{
	public partial class Login : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			btnLogin.Click += btnLogin_OnClick;
			btnForgotPassword.Click += btnForgotPassword_OnClick;
		}

		private void btnForgotPassword_OnClick(object sender, EventArgs e)
		{
			IMailSender mailSender = ObjectFactory.GetInstance<IMailSender>();

			MailMessage mailMessage = new MailMessage();

			mailMessage.From = new MailAddress("khurwitz@hotmail.com");
			mailMessage.To.Add(txtEmailAddress.Text);
			mailMessage.Body = "Your password is abc";
			mailMessage.Subject = "Your forgotten password";

			mailSender.SendMail(mailMessage);
		}

		private void btnLogin_OnClick(object sender, EventArgs e)
		{
			string emailAddress = txtEmailAddress.Text;
			string password = txtPassword.Text;

			if ((emailAddress == "abc") && (password == "abc"))
			{
				FormsAuthentication.RedirectFromLoginPage(emailAddress, chkRememberMe.Checked);
			}
			else
			{
				lblResults.Text = "Invalid Credentials: Please try again";
			}
		}
	}
}