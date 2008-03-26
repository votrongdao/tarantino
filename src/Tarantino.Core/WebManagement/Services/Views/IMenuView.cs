using StructureMap;
using Tarantino.Core.Commons.Services.DataFileManagement;
using Tarantino.Core.Commons.Services.Environment;

namespace Tarantino.Core.WebManagement.Services.Views
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IMenuView
	{
		string BuildHtml();
	}
}