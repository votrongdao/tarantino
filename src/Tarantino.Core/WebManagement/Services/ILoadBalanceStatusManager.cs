using StructureMap;

namespace Tarantino.Core.WebManagement.Services
{
	[PluginFamily(Keys.Default)]
	public interface ILoadBalanceStatusManager
	{
		string HandleLoadBalanceRequest();
	}
}