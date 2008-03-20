using StructureMap;

namespace Tarantino.Core.WebManagement.Services.Views
{
	[PluginFamily(Keys.Default)]
	public interface ILoadBalancerView
	{
		void Render(string errorMessage);
	}
}