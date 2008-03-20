using System.Net.Mail;
using StructureMap;

namespace Tarantino.Core.Commons.Services.Security
{
	[PluginFamily(Keys.Default)]
	public interface IForgottenPasswordMailFactory
	{
		MailMessage CreateEmail(string recipientEmailAddress, string clearTextPassword);
	}
}