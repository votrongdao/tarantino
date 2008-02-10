using StructureMap;

namespace Tarantino.Commons.Core.Services.Net
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IWebDataReader
	{
		string ReadUrl(string url, string parameterName, string parameterValue);
	}
}