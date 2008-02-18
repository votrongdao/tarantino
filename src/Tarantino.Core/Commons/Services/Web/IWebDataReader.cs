using StructureMap;

namespace Tarantino.Core.Commons.Services.Web
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IWebDataReader
	{
		string ReadUrl(string url, string parameterName, string parameterValue);
	}
}