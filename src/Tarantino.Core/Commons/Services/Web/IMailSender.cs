using System.Net.Mail;
using StructureMap;

namespace Tarantino.Commons.Core.Services.Net
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IMailSender
	{
		void SendMail(MailMessage message);
	}
}