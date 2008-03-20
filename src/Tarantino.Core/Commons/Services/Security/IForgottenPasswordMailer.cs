using StructureMap;
using Tarantino.Core;
using Tarantino.Core.Commons.Services.Repositories;

namespace Tarantino.Core.Commons.Services.Security
{
	[PluginFamily(Keys.Default)]
	public interface IForgottenPasswordMailer
	{
		bool SendForgottenPasswordEmail(string recipientEmailAddress, ISystemUserRepository repository);
	}
}