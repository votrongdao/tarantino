using System.Net.Mail;
using StructureMap;

namespace Tarantino.Core.Commons.Services.Web
{
	[PluginFamily(Keys.Default)]
	public interface IMailSender
	{
		void SendMail(MailMessage message);
	}
}