using StructureMap;
using Tarantino.Core.Commons.Services.Repositories;

namespace Tarantino.Core.Commons.Services.Security
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IForgottenPasswordService
	{
		string SendEmailTo(string emailAddress, ISystemUserRepository repository);
	}
}