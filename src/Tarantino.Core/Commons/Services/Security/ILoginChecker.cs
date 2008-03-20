using StructureMap;
using Tarantino.Core.Commons.Services.Repositories;

namespace Tarantino.Core.Commons.Services.Security
{
	[PluginFamily(Keys.Default)]
	public interface ILoginChecker
	{
		bool IsValidUser(string emailAddress, string clearTextPassword, ISystemUserRepository repository);
	}
}