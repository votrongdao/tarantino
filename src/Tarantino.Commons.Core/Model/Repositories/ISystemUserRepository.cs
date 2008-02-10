using StructureMap;

namespace Tarantino.Commons.Core.Model.Repositories
{
	[PluginFamily(ServiceKeys.Default)]
	public interface ISystemUserRepository
	{
		bool IsValidLogin(string emailAddress, string password);
		SystemUser GetByEmailAddress(string emailAddress);
	}
}