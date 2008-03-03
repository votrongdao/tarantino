using StructureMap;

namespace Tarantino.Core.WebManagement.Services.Views
{
	[PluginFamily(ServiceKeys.Default)]
	public interface ILoadBalancerBodyView
	{
		string BuildHtml(string errorMessage);
	}
}