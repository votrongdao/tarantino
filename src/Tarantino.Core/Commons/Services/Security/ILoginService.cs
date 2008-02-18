using StructureMap;
using Tarantino.Core.Commons.Services.Repositories;

namespace Tarantino.Core.Commons.Services.Security
{
	[PluginFamily(ServiceKeys.Default)]
	public interface ILoginService
	{
		string Login(string emailAddress, string password, bool rememberMe, ISystemUserRepository repository);
		void Logout();
	}
}