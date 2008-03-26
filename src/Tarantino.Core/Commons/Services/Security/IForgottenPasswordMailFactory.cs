using System.Net.Mail;
using StructureMap;

namespace Tarantino.Core.Commons.Services.Security
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IForgottenPasswordMailFactory
	{
		MailMessage CreateEmail(string recipientEmailAddress, string clearTextPassword);
	}
}