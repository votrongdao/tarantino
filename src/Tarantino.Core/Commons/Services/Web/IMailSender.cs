using System.Net.Mail;
using StructureMap;

namespace Tarantino.Core.Commons.Services.Web
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IMailSender
	{
		void SendMail(MailMessage message);
	}
}