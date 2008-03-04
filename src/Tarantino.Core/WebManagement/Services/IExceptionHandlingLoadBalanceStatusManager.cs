using StructureMap;

namespace Tarantino.Core.WebManagement.Services
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IExceptionHandlingLoadBalanceStatusManager
	{
		void HandleLoadBalancing();
	}
}