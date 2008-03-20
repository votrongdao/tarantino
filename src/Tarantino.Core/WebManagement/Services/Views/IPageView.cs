using StructureMap;

namespace Tarantino.Core.WebManagement.Services.Views
{
	[PluginFamily(Keys.Default)]
	public interface IPageView
	{
		string BuildHtml(string bodyHtml);
	}
}