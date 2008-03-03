using StructureMap;

namespace Tarantino.Core.WebManagement.Services.Views
{
	[PluginFamily(ServiceKeys.Default)]
	public interface ILoadBalancerView
	{
		void Render(string errorMessage);
	}
}