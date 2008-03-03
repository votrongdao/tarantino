using StructureMap;

namespace Tarantino.Core.WebManagement.Services.Views
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IPageView
	{
		string BuildHtml(string bodyHtml);
	}
}